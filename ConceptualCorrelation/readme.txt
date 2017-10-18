----Conceptual Correlation Metric----


Proof of concept - provided with no warranty or support. Use at your own risk.

Written by Jason Gorman, Codemanship Limited, www.codemanship.com (2017)


-----------------------------------------------------------------------------------------------------------------

This simple prototype calculates the % of words used in the names in your code (class names, method names, variables etc) that are also used in requirements text.

It helps to understand the conceptual correlation between code and requirements if we're attempting to be more problem-driven (or "domain-driven") in our solution designs, and write code that communicates its intent more clearly.



This is a command-line tool. To use it, simply call:


Conceptual.exe "<file name of .NET .dll or .exe to analyse>" "<file name of requirements .txt to compare against>"


e.g., 


Conceptual "C:\MyProjects\FlightBooking\bin\debug\FlightBooking.dll" "C:\MyProjects\FlightBooking\usecases.txt"


IMPORTANT: .NET assemblies must have an associated .pdb file in the same directory


If you try this tool and want to share your findings or ask any questions about Conceptual Correlation, I can be emailed at jason.gorman@codemanship.com or tweeted @jasongorman. This does not constitute an offer of warranty or technical support.