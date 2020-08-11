Public Class SimplSerialFlasher
    Public Event Logger(type As String, msg As String)

    Private _sserial As SimplSerialBusLight

    Public ReadOnly Property SSerial As SimplSerialBusLight
        Get
            Return _sserial
        End Get
    End Property

    Public Sub New(sserial As SimplSerialBusLight)
        _sserial = sserial
    End Sub

    Public Property SpmSize As Integer
    Public Property ProgmemSize As Integer
    'Public Property Signature As String = ""
    'Public Property Fuses As String = ""

    Public Sub EraseAll(address As Integer)
        Dim spm = SpmSize
        Dim size = ProgmemSize - 1024 * 4
        For i = 0 To size - 1 Step spm
            Dim page As Integer = Math.Floor(i \ spm)
            RaiseEvent Logger("INF", "Erase: " + i.ToString + "\" + size.ToString)
            ErasePage(address, page)
        Next
    End Sub

    Public Shared Function LoadFirmwareFromHexString(hexString As String) As Byte()
        Dim str = hexString.Split({vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
        Dim converter = New HexToBinConverter()
        Dim bin = converter.Hex2Bin(str)
        Return bin
    End Function

    Public Shared Function LoadFirmwareFromFile(file As String) As Byte()
        Dim bin As Byte()
        Dim ext = IO.Path.GetExtension(file).ToLower
        Select Case ext
            Case ".hex"
                Dim str = IO.File.ReadAllLines(file)
                Dim converter = New HexToBinConverter()
                bin = converter.Hex2Bin(str)
            Case ".bin"
                bin = IO.File.ReadAllBytes(file)
            Case Else
                Throw New Exception("Extension Not supported. Use HEX Or BIN.")
        End Select
        Return bin
    End Function

    Private _rnd As New Random

    Public Sub AutoFlash(guid As Guid, firmware As Byte(), Optional fastMode As Integer = 0)
        Dim address = _rnd.Next(1, 30000)
        _sserial.RequestSetAddress(guid, address)

        Try
            _sserial.RequestGoToBootloader(address)
        Catch ex As Exception
        End Try

        _sserial.RequestSetAddress(guid, address)
        RequestBootInfo(address)
        If fastMode > 0 Then
            EraseAndFlashAllFast(address, firmware, fastMode)
        Else
            EraseAndFlashAll(address, firmware)
        End If
    End Sub

    Public Sub AutoFlash(address As Integer, firmware As Byte(), Optional fastMode As Integer = 0)
        Try
            _sserial.RequestGoToBootloader(address)
        Catch ex As Exception
        End Try
        RequestBootInfo(address)
        If fastMode > 0 Then
            EraseAndFlashAllFast(address, firmware, fastMode)
        Else
            EraseAndFlashAll(address, firmware)
        End If
    End Sub

    Public Sub EraseAndFlashAll(address As Integer, bin As Byte())
        EraseAll(address)
        FlashAll(address, bin)
    End Sub

    Public Sub FlashAll(address As Integer, bin As Byte())
        Dim binLength As Integer
        binLength = bin.Length
        Dim spm = SpmSize
        Dim size = ProgmemSize - 1024 * 4
        If spm < 64 Then Throw New Exception("SPM = 0")

        Dim binsize As Integer = Math.Ceiling(bin.Length / spm) * spm
        ReDim Preserve bin(binsize - 1) '

        For i = 0 To bin.Length - 1 Step spm
            Dim page As Integer = Math.Floor(i \ spm)
            RaiseEvent Logger("INF", "Program:   " + i.ToString + "\" + binLength.ToString)
            EraseFillWritePage(address, page, bin, i, spm)
        Next
    End Sub

    Public Sub RequestBootInfo(address As Integer)
        Dim info = _sserial.Request(New SSRequest(address, 100, {}))
        If info.ResponseState = ResponseState.ok Then
            Dim spm = info.Data(16) * 256 + info.Data(17)
            Dim pgmsize = info.Data(18) * 256 * 256 + info.Data(19) * 256 + info.Data(10)
            'Dim sign = info.Data(0) * 256 * 256 + info.Data(1) * 256 + info.Data(2)
            SpmSize = spm
            ProgmemSize = Math.Floor(pgmsize / SpmSize) * SpmSize
            'Signature = "0x" + Hex(sign)
            'Select Case Signature
            'Case "0x1E9511" : Signature += "," + "ATmega324PA"
            'End Select
            'Fuses = "0x" + Hex(info.Data(3)) + "," + "0x" + Hex(info.Data(6)) + "," + "0x" + Hex(info.Data(5)) + "//L,H,E"
        Else
            Throw New Exception(info.ResponseState.ToString)
        End If
    End Sub

    Public Sub ErasePage(address As Integer, page As Integer)
        Dim page0 = (page >> 8) And 255
        Dim page1 = page Mod 256
        Dim test = _sserial.Request(New SSRequest(address, 102, {page0, page1, 0}), 50)
        If test.ResponseState <> ResponseState.ok Then Throw New Exception(test.ResponseState.ToString)
        If test.Result <> 103 Then Throw New Exception(test.ResponseState.ToString)
        RaiseEvent Logger("DBG", "ErasePage")
    End Sub

    Public Sub FillPageBuffer(address As Integer, page As Integer, offset As Integer, bytes As Byte())
        If bytes.Length <> 8 Then Throw New Exception("FillPageBuffer <> 8")
        Dim page0 = (page >> 8) And 255
        Dim page1 = page Mod 256
        Dim offset0 = (offset >> 8) And 255
        Dim offset1 = offset Mod 256
        Dim test = _sserial.Request(New SSRequest(address, 104, {page0, page1, offset0, offset1, bytes(0), bytes(1), bytes(2), bytes(3), bytes(4), bytes(5), bytes(6), bytes(7)}), 50)
        If test.ResponseState <> ResponseState.ok Then Throw New Exception(test.ResponseState.ToString)
        If test.Result <> 105 Then Throw New Exception(test.ResponseState.ToString)
        RaiseEvent Logger("DBG", "FillPage")
    End Sub

    Public Sub WritePage(address As Integer, page As Integer)
        Dim page0 = (page >> 8) And 255
        Dim page1 = page Mod 256
        Dim test = _sserial.Request(New SSRequest(address, 106, {page0, page1}), 50)
        If test.ResponseState <> ResponseState.ok Then Throw New Exception(test.ResponseState.ToString)
        If test.Result <> 107 Then Throw New Exception(test.ResponseState.ToString)
    End Sub

    Public Sub EraseFillWritePage(address As Integer, page As Integer, data As Byte(), offset As Integer, size As Integer)
        If size <> 128 And size <> 64 And size <> 256 Then Throw New Exception("EraseFillWritePage:  size <> 128, size <> 64")
        If data.Length < offset + size Then Throw New Exception("EraseFillWritePage: Data not enough")

        Dim buffer(size - 1) As Integer
        For i = 0 To buffer.Length - 1
            buffer(i) = data(offset + i)
        Next

        ErasePage(address, page)
        For i = 0 To size - 1 Step 8
            FillPageBuffer(address, page, i, {buffer(i), buffer(i + 1), buffer(i + 2), buffer(i + 3), buffer(i + 4), buffer(i + 5), buffer(i + 6), buffer(i + 7)})
        Next
        WritePage(address, page)
    End Sub

#Region "Fastmode"
    Public Sub EraseAndFlashAllFast(address As Integer, bin As Byte(), Optional fastmode As Integer = 32)
        EraseAllFast(address)
        FlashAllFast(address, bin, fastmode)
    End Sub

    Public Sub EraseAllFast(address As Integer)
        Dim timeout = _sserial.RequestTimeout
        _sserial.RequestTimeout = 10000
        RaiseEvent Logger("INF", "Erase All Flash (Fast)...")
        Dim test = _sserial.Request(New SSRequest(address, 110, {0, 0, 0, 0}), 1)
        _sserial.RequestTimeout = timeout
        If test.ResponseState <> ResponseState.ok Then Throw New Exception(test.ResponseState.ToString)
        If test.Result <> 111 Then Throw New Exception(test.Result.ToString)
    End Sub

    Public Sub FillPageBufferFast(address As Integer, page As Integer, offset As Integer, buffer As Byte(), bufferOffset As Integer, bufferCount As Integer)
        If bufferCount Mod 2 Then Throw New Exception("FillPageBuffer Mod 2 != 0")
        Dim page0 = (page >> 8) And 255
        Dim page1 = page Mod 256
        Dim offset0 = (offset >> 8) And 255
        Dim offset1 = offset Mod 256
        Dim databytes As New List(Of Byte)
        databytes.AddRange({page0, page1, offset0, offset1})
        For i = 0 To bufferCount - 1
            databytes.Add(buffer(bufferOffset + i))
        Next
        Dim test = _sserial.Request(New SSRequest(address, 108, databytes.ToArray), 50)
        If test.ResponseState <> ResponseState.ok Then Throw New Exception(test.ResponseState.ToString)
        If test.Result <> 109 Then Throw New Exception(test.Result.ToString)
        RaiseEvent Logger("DBG", "FillPageFast")
    End Sub

    Public Sub FlashAllFast(address As Integer, bin As Byte(), fastmode As Integer)
        Dim binLength As Integer
        binLength = bin.Length
        Dim spm = SpmSize
        Dim size = ProgmemSize - 1024 * 4
        If spm < 64 Then Throw New Exception("SPM = 0")

        Dim binsize As Integer = Math.Ceiling(bin.Length / spm) * spm
        ReDim Preserve bin(binsize - 1) '

        For i = 0 To bin.Length - 1 Step spm
            Dim page As Integer = Math.Floor(i \ spm)
            RaiseEvent Logger("INF", "Program (Fast): " + i.ToString + "\" + binLength.ToString)
            FillWritePageFast(address, page, bin, i, spm, fastmode)
        Next
    End Sub

    Public Sub FillWritePageFast(address As Integer, page As Integer, data As Byte(), offset As Integer, size As Integer, fastmode As Integer)
        If size <> 128 And size <> 64 And size <> 256 Then Throw New Exception("EraseFillWritePageFast:  size <> 128, 64, 256")
        If data.Length < offset + size Then Throw New Exception("EraseFillWritePageFast: Data not enough")

        Dim buffer(size - 1) As Byte
        For i = 0 To buffer.Length - 1
            buffer(i) = data(offset + i)
        Next

        For i = 0 To size - 1 Step fastmode
            FillPageBufferFast(address, page, i, buffer, i, fastmode)
        Next
        WritePage(address, page)
    End Sub
#End Region

End Class

Public Class HexToBinConverter
    Private _segmAddr As Integer
    Private _advAdr As Integer
    Private _bytes(0) As Byte

    Public Function Hex2Bin(hexStrings As String()) As Byte()
        For i = 0 To hexStrings.Length
            If hexStrings(i) = ":00000001FF" Then
                Return _bytes
            End If
            Dim hexData = HexStringToData(hexStrings(i))
            If hexData.Type = 4 Then
                _advAdr = hexData.Data(0)
                _advAdr = _advAdr << 8
                _advAdr += hexData.Data(1)
            ElseIf hexData.Type = 2 Then
                _segmAddr = hexData.Data(0)
                _segmAddr = _advAdr << 8
                _segmAddr += hexData.Data(1)
            ElseIf hexData.Type = 0 Then
                Dim addr As Long = _segmAddr << 16
                addr += hexData.AddrByte1
                addr = addr << 8
                addr += hexData.AddrByte2

                If _bytes.Length < (addr + hexData.DataLen) Then
                    ReDim Preserve _bytes(addr + hexData.DataLen - 1)
                End If

                Array.Copy(hexData.Data, 0, _bytes, addr, hexData.DataLen)
            End If
        Next
        Throw New Exception("Плохой конец файла")
    End Function

    Public Shared Sub CheckCrcAndLengthGood(bytes As Byte())
        'Dim sum As Byte = 0
        'For i = 0 To bytes.Length - 2
        '	sum = sum Xor bytes(i)
        'Next
        'Dim crc1 As Byte = 255 - (Not sum)
        'Dim crc2 = bytes.Last
        'Dim res = False
        'If crc1 = crc2 Then
        '	Dim dataLen1 = bytes(0)
        '	Dim dataLen2 = bytes.Length - 5
        '	If dataLen1 = dataLen2 Then
        '		'Return True
        '	Else
        '		Throw New Exception("Bad data length")
        '	End If
        'Else
        '	Throw New Exception("Bad crc")
        'End If
    End Sub

    Private Function HexStringToBytes(hexString As String) As Byte()
        If hexString(0) = ":" Then
            Dim bytes As New List(Of Byte)
            Dim correct As Boolean = True
            For i = 1 To hexString.Length - 1 Step 2
                Dim part = Mid(hexString, i + 1, 2)
                Try
                    Dim b = CByte(Val("&H" + part))
                    bytes.Add(b)
                Catch ex As Exception
                    Throw New Exception("Bad String Format, not sequense of hex")
                End Try
            Next
            Return bytes.ToArray
        Else
            Throw New Exception("Bad String Format, нет символа ':'")
        End If
    End Function

    Private Function HexStringToData(hexString As String) As HexData
        Dim bytes = HexStringToBytes(hexString)
        CheckCrcAndLengthGood(bytes)
        Dim data = New HexData
        data.DataLen = bytes(0)
        data.AddrByte1 = bytes(1)
        data.AddrByte2 = bytes(2)
        data.Type = bytes(3)
        Dim mas(data.DataLen - 1) As Byte
        Array.Copy(bytes, 4, mas, 0, data.DataLen)
        data.Data = mas
        data.Crc = bytes.Last
        Return data
    End Function

    Class HexData
        Public Property DataLen As Byte
        Public Property AddrByte1 As Byte
        Public Property AddrByte2 As Byte
        Public Property Type As Byte
        Public Property Data As Byte()
        Public Property Crc As Byte
    End Class
End Class

