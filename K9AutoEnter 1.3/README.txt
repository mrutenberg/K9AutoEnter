# K9AutoEnter
Console* application to automatically enter a list of URLs into the K9 Web Protection filter exceptions list

# Explanation
K9 Web Protection Filter doesnt allow to bulk import URLs into the exceptions (always allow) list.  
Manually entering large ammounts of URLs gets very tedious expecially if done on many PC's

I built a small application in c# using Watin that will open K9 in IE the supplied password and enter the list of URLs in a specified text file

# Todo
*Change to winforms/WPF?  
Fix BlockList exceptions 


#---------------------------------------------
TO ADD SITES TO THE EXCEPTIONS LIST:

Allow Sites: Edit or add sites in the K9AllowList.txt file
Block Sites: Edit or add sites in the K9BlockList.txt file - not working

#---------------------------------------------