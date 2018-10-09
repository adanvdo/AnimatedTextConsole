<b>AnimatedTextConsole by JAMGALACTIC</b>
<p>AnimatedTextConsole is a simple .NET Console Application that allows you to display animated, typewriter-style text in the Windows CMD.</p>
<p>The purpose of this app was to assist with the creation of the text animations with the intent of being recorded by screen-recording software like OBS Studio. My personal reason for writing this program was for creating android boot animations with a terminal style text animation.</p>
<p>To use the program</p>
<p>Run AnimatedTextConsole from the command line.  The only required argument is the -txt argument which specifies a text file to read lines of text from. Several other optional arguments can be used.<br>
Example: c:\program directory> AnimatedTextConsole -txt "c:\linesoftext.txt" 
</p>
<p>Complete List of Arguments</p>
<table>
<tr>
<th>Arg</th>
<th>Description</th>
<th>Usage</th>
</tr>
<tr><td>-cd</td><td>set delay in ms between characters</td><td>-cd 30</td></tr>
<tr><td>-ld</td><td>set delay in ms before next line</td><td>-ld 800</td></tr>
<tr><td>-rld</td><td>randomize line delay between by +/- 500 ms</td><td>-rld</td></tr>
<tr><td>-ds</td><td>enable double spacing between lines</td><td>-ds</td></tr>
<tr><td>-txt</td><td>set txt filepath with lines to display</td><td>-txt "c:\users\bobdole\downloads\textlines.txt"</td></tr>
<tr><td>-w</td><td>set window width in columns</td><td>-w 50</td></tr>
<tr><td>-h</td><td>set window height in columns</td><td>-h 50</td></tr>
<tr><td>-cbl</td><td>clear console before last line of text</td><td>-cbl</td></tr>
</table>
<p>Program Execution Example</p>
<img src="https://github.com/adanvdo/AnimatedTextConsole/blob/master/AnimatedTextConsole/command.jpg"><br>
<img src="https://github.com/adanvdo/AnimatedTextConsole/blob/master/AnimatedTextConsole/animatedtextconsole.gif">
