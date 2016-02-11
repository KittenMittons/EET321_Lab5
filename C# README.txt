1. Load the vivado project that is needed for the specific measurement.

2. Adjust the frequency until the the DC line stops oscillating.

4. Before you take your measurement fill out the board Ids and the group Id textbox. *IMPORTANT*
	(There is an example Excel document that shows what is needed)

5. Press appropriate measurement button.

6. Either hit submit right then, or wait to submit all at once when you are finished working. It is your choice.

7. As with before,  if you want to also copy down the frequency of the function generator it'd be a nice backup.

8. For the Direct measurements, you need to use a digital oscilloscope.



If you didn't hit submit last lab:

1. Turn the function generator to the exact frequency that you wrote down. MAKE SURE IT IS NOT THE TEXTBOX VALUE. If you only wrote down the textbox value, adjust the function generator by looking at what the count is for the specific measurement and dividing the textbox frequency by that count. Then set the function generator to that value instead. It's okay to use, it's just going to be a factor of +- 99Hz off because of the conversion from float to string truncating after about 7 decimal places.

Count values
50MHz 8KHz:	6250
50MHz 16kHz:	3125
125MHz	8KHz:	15625
125MHz	16KHz:	7831

Example:
Textbox value/count = function generator frequency

	50,000,120 / 6250 =	8000.0192 KHz 			



2. Press the appropriate button to re-take the measurement.
