/*
 * Bwl I2C Lib
 *
 * Author: Igor Koshelev 
 * Licensed: open-source Apache license
 *
 * Version: 01.07.2016
 */ 
#include <avr/io.h>

#ifndef TWCR
#define TWCR TWCR0
#define TWBR TWBR0
#define TWDR TWDR0
#define TWSR TWSR0

#endif

void i2c_wait()
{
	volatile char i=0;
	while ((!(TWCR & (1 << TWINT)))&(i<250)){i++;}
}

void i2c_start() {
	TWCR = (1 << TWINT) | (1 << TWSTA) | (1 << TWEN);

	i2c_wait();
}

void i2c_write_byte(char byte) {
	TWDR = byte;
	TWCR = (1 << TWINT) | (1 << TWEN);
	i2c_wait();
}

char i2c_read_byte() {
	TWCR = (1 << TWINT) | (1 << TWEA) | (1 << TWEN);
	i2c_wait();
	return TWDR;
}

char i2c_read_last_byte() {
	TWCR = (1 << TWINT) | (1 << TWEN);
	i2c_wait();
	return TWDR;
}

void i2c_stop() {
	TWCR = (1 << TWINT) | (1 << TWSTO) | (1 << TWEN);
}

void i2c_init(int clock_divider)
{
	if (clock_divider<16){clock_divider=16;}
	//TWSR – TWI Status Register
	TWSR = 0;
	//TWBR – TWI Bit Rate Register
	TWBR = (clock_divider-16)/2;
}