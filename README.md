# EET321 Lab 5

##Equipment list:
- [A PC](http://www.marmaxcomputers.com/img/080715121844dell_precision_t3500.pdf)

- [Zybo Board](https://reference.digilentinc.com/_media/zybo:zybo_rm.pdf)

- [PMOD DA2](https://reference.digilentinc.com/_media/pmod:pmod:pmodDA2_rm.pdf)

- [An oscilloscope](http://mil.ufl.edu/4712/docs/Oscope_Tek2235-op.pdf)

- [A waveform generator](https://www.bu.edu/eng/courses/ek307/documents/hp33120a.pdf)

- USB type A to micro USB cable

- Oscilloscope Probe (BNC connector type)

- BNC to alligator clip cable

- 4 Solid core wires

- GPIB cable


##Procedure:

1. Plug your zybo board into your PC and connect the DA2 PMOD into the upper section of the Zybo JC port.
2. Unzip file containing the 4 Vivado projects and open the first one in the 50MHz folder names "8KHz".
3. Download the bitstream to the board.
4. Open the included c# application.
5. Turn on your waveform generator and set it to 8KHz with an amplitude 600mVpp with an offset of 400mVDC. Use the oscillscope to ensure that the wave never goes below zero and is the amplitude desired. :warning: **Do not trust the amplitude listed on the front of your waveform generator.** Set the GPIB address of the waveform generator to address 1.
6. Connect the waveform generator to the XADC of the JA port. Connect the positive alliagtor clip to the JA4 pin on the board, and the negative alligator clip to the JA10 pin. Refer to the zybo documentaion listed above for where these pins are.
7. Connect the oscilloscope to pin J0 on the front of the PMOD DA2. You can ground the oscilloscope probe on any ground pin on the zybo board, but the closest one is pin J5 on the DA2.
8. If everything was done correctly you should an output very close to a flat DC output. Change the frequency of the waveform generator ver slightly until you get a perfectly flat output.
9. In the C# application select the "board one" option from the menu and click the "8KHz" button under the "50MHz" label.
10. Repeat these steps for "16KHz/50MHz", "8KHz/125MHz", and "16KHz/125MHz".
11. Now do all of this over again for your partner's board.
12. Make all 8 measurements over again, giving you a total of 16 measurements so far. Don't give up you're doing great! :clap: :tada: :star2:
