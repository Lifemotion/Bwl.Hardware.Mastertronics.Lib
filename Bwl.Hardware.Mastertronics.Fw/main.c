#define F_CPU 16000000UL
#define BAUD 38400
#define DEV_NAME "Bwl.Mastertronics.Fw.1.1"

//#define FUSES_VALUE  { 0xDE, 0xC0 , 0xFD, }// low high extended

#include <avr/boot.h>
#include <avr/io.h>
#include <avr/wdt.h>
#include <util/setbaud.h>
#include <util/delay.h>

#include "refs/bwl_uart.h"
#include "refs/bwl_simplserial.h"

int32_t stepper_counter[4];
#define sbi(port, bit)			(port) |=  (1 << (bit))
#define cbi(port, bit)			(port) &= ~(1 << (bit))
#define getbit(port, bit)		((port) &   (1 << (bit)))
#define setbit(port,bit,val)	{if ((val)) {(port)|= (1 << (bit));} else {(port) &= ~(1 << (bit));}}
	
void sserial_send_start(unsigned char portindex)
{
	//TX_START_MACRO
}

void sserial_send_end(unsigned char portindex)
{
	//TX_END_MACRO
}

void stepper_x(byte enable, byte dir, byte step)
{
	sbi(DDRD,7); setbit(PORTD,7,enable);
	sbi(DDRF,1); setbit(PORTF,1,dir);
	sbi(DDRF,0); setbit(PORTF,0,step);		
}

void stepper_y(byte enable, byte dir, byte step)
{
	sbi(DDRF,2); setbit(PORTF,2,enable);
	sbi(DDRF,7); setbit(PORTF,7,dir);
	sbi(DDRF,6); setbit(PORTF,6,step);
}

void stepper_z(byte enable, byte dir, byte step)
{
	sbi(DDRK,0); setbit(PORTK,0,enable);
	sbi(DDRL,1); setbit(PORTL,1,dir);
	sbi(DDRL,3); setbit(PORTL,3,step);
}

void stepper(byte number,byte enable, byte dir, byte step)
{
	switch (number)
	{
		case 1: stepper_x(enable,dir,step);if (step) {if (dir==0) {stepper_counter[number]++;} else {stepper_counter[number]--;}}		break;
		case 2: stepper_y(enable,dir,step);if (step) {if (dir==0) {stepper_counter[number]++;} else {stepper_counter[number]--;}}		break;
		case 3: stepper_z(enable,dir,step);if (step) {if (dir==0) {stepper_counter[number]++;} else {stepper_counter[number]--;}}		break;	
	}
}

uint32_t get_stepper_position(byte number)
{
	switch (number)
	{
		case 1: return stepper_counter[number];	break;
		case 2: return stepper_counter[number];	break;
		case 3: return stepper_counter[number];	break;
	}	
	return -1;
}

void stepper_delay(int step_delay_50us)
{
	for (int i=1; i<step_delay_50us; i++)	
	{
		_delay_us(50.0);
	}
}

byte blocker(byte number, byte direction)
{
	if (direction==1)
	{
		if (number==1){cbi(PORTE,5); sbi(PORTE,5); return getbit(PINE,5)==0;}
		if (number==2){cbi(PORTJ,1); sbi(PORTJ,1); return getbit(PINJ,1)==0;}
		if (number==3){cbi(PORTD,3); sbi(PORTD,3); return getbit(PIND,3)==0;}	
	}else
	{
		if (number==1){cbi(PORTE,4); sbi(PORTE,4); return getbit(PINE,4)==0;}
		if (number==2){cbi(PORTJ,0); sbi(PORTJ,0); return getbit(PINJ,0)==0;}
		if (number==3){cbi(PORTD,2); sbi(PORTD,2); return getbit(PIND,2)==0;}		
	}
	return 0;
}

void stepper_work(byte number, int steps_to_make, byte free_at_end, int step_delay_100us)
{
	byte direction=0;
	if (steps_to_make<0){direction=1; steps_to_make=-steps_to_make;}
	for (int i=0; i<steps_to_make; i++)
	{
		if (blocker(number,direction)==0)
		{
			stepper(number,0,direction,1);
			stepper_delay(step_delay_100us);
			stepper(number,0,direction,0);
			stepper_delay(step_delay_100us);
		}
		wdt_reset();
	}
	if (free_at_end!=0)
	{
		stepper(number,1,0,0);
	}
}

byte stepper_blockers()
{
	byte result=0;
	if (blocker(1,1)){result|=1;}
	if (blocker(1,0)){result|=2;}
	if (blocker(2,1)){result|=4;}
	if (blocker(2,0)){result|=8;}
	if (blocker(3,1)){result|=16;}
	if (blocker(3,0)){result|=32;}		
	return result;
}

void var_delay_ms(int ms)
{
	for (int i=0; i<ms; i++)_delay_ms(1.0);
}

void sserial_process_request(unsigned char portindex)
{
	//PING
	if (sserial_request.command==80)
	{
		sserial_response.result=128+sserial_request.command;	
		sserial_response.data[0]=1;
		sserial_response.datalength=1;
		sserial_send_response();
	}	
	//STEPPER POS RESET
	if (sserial_request.command==84)
	{
		stepper_counter[1]=0;
		stepper_counter[2]=0;
		stepper_counter[3]=0;
		
		sserial_response.result=128+sserial_request.command;	
		sserial_response.data[0]=1;
		sserial_response.datalength=1;
		sserial_send_response();		
	}
	
	//STEPPER GET STATE
	if (sserial_request.command==88)
	{
		byte motor=sserial_request.data[0];
			
		sserial_response.result=128+sserial_request.command;	
		uint32_t pos=get_stepper_position(motor);
		sserial_response.data[0]=(pos>>24)&255;
		sserial_response.data[1]=(pos>>16)&255;
		sserial_response.data[2]=(pos>>8)&255;
		sserial_response.data[3]=(pos)&255;
		sserial_response.datalength=4;
		sserial_send_response();
	}	
	
	//STEPPER	
	if (sserial_request.command==81)
	{
		sserial_response.result=128+sserial_request.command;
		sserial_response.data[0]=1;
		sserial_response.datalength=1;
		sserial_send_response();
		byte motor=sserial_request.data[0];
		int  steps=(sserial_request.data[1]<<8)|sserial_request.data[2];
		byte dir=sserial_request.data[3]; if (dir>0){steps=-steps;}
		int delay =(sserial_request.data[4]<<8)|sserial_request.data[5];
		byte freem=sserial_request.data[6];
		stepper_work(motor,steps,freem,delay);
		sserial_response.result=128+sserial_request.command;
		
		uint32_t pos=get_stepper_position(motor);
		sserial_response.data[0]=(pos>>24)&255;
		sserial_response.data[1]=(pos>>16)&255;
		sserial_response.data[2]=(pos>>8)&255;
		sserial_response.data[3]=(pos)&255;
		sserial_response.datalength=4;
		sserial_send_response();
	}
	//GET STEPPER BLOCKERS
	if (sserial_request.command==101)
	{
		sserial_response.result=128+sserial_request.command;
		stepper_blockers();
		sserial_response.data[0]=stepper_blockers();
		sserial_response.datalength=1;		
		sserial_send_response();		
	}
}

void motorboard_run_infinite()
{
	while(1)
	{
		sserial_poll_uart(0);
		wdt_reset();
	}
}

void motorboard_init()
{
	stepper_blockers();
	wdt_enable(WDTO_8S);
	uart_init_withdivider(0,UBRR_VALUE);
}

int main(void)
{	
	motorboard_init();
		
	for (int i=0; i<32; i++) sserial_devname[i]=DEV_NAME[i];
	sbi(DDRB,7);cbi(PORTB,7);
	//return;
	//stepper_work(2,-10000,1,3);
	//stepper_work(2,10000,1,3);
	//while(1)wdt_reset();
	//uart_loopback_infinite();
	motorboard_run_infinite();
}



