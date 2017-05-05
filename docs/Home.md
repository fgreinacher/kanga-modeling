# What is KangaModeling?
**KangaModeling allows you to produce UML (like) diagrams very easily.**
The main idea is to generate the diagram from a lightweight markup textual diagram description language.

Following code:
```
title Hello 
participant Alice
Alice->Bob : Hi!
activate Bob
Bob-->Alice : Hey!
deactivate Bob
```
produces this diagram:
![](Home_sd_Hello.png)

        See KangaModeling **[Documentation](Documentation)** for full language tutorial.
        
# Key Features
* **No mouse kicking** - KangaModeling is made for those who think that full-fledged modeling tools are overdesigned for visualizing communication and relations between software components.
* **Always up to date** - if you use a diagram in wiki or blog it is often additional overhead to open a modeling tool, update the diagram, export to image and upload it back.
* That's why we provide a **web service** which will produce digarm image from markup in your blog on the fly. You can use our web service at [KangaModeling.org](http://kangamodeling.org). 
* Alternatively if you are looking for an intranet solution - you are welcome to download our ASP.NET **web application** and host it yourself (for free).        

## Additional Features:
* _KangaModeling.Compiler_ helps you to generate consistent diagram by producing error messages and hints.
* _KangaModeling.GuiRunner_ is a demo application which can also be used to create diagrams.
* _KangaModeling.WebRunner_ allows you to generate diagrams from markup direkt into your wiki or blog see also [KangaModeling.org](http://kangamodeling.org).

# Etc.
* KangaModeling is built upon the .NET platform with a strong focus on testabilty, maintainabilty and developer usabilty.
* KangaModeling if free to use - take a look at the [License](http://kangamodeling.codeplex.com/license) for more details. 
* We ask you kindly to include copyright notice and a link to [KangaModeling.org](http://kangamodeling.org) in all copies or substantial portions of the software.

# How can I help?
* Please report us all issues you are experiencing during using KangaModeling using the [Issue Tracker](http://kangamodeling.codeplex.com/workitem/list/basic).
* Features you miss or are nice to have can also be reported under  [Issue Tracker](http://kangamodeling.codeplex.com/workitem/list/basic).
* Please rate (vote) issues you think can help improving KangaModeling.

# Future Plans
* Extending Sequence Diagram Language
* Supporting Class Diagram
* Supporting mode designs (currently only Sketchy)
* Wordpress Plug-In
* Wiki Template
* All in one Forms Control
* All in one WPF Control

# Our Sponsors        
**We would like to thank our sponsors for their engagement.**

|![DiscountASP.NET](Home_DiscountAspNet.gif|http://www.discountasp.net)|
|[DiscountASP.NET](http://www.discountasp.net) - for providing us with ASP.NET hosting for our web service [KangaModeling.org](http://kangamodeling.org)|
| |
|![JetBrains ReShaper](Home_ReSharperBanner.png|http://www.jetbrains.com)|
|[JetBrains](http://www.jetbrains.com) - for providing us with ReSharper Full Edition license [ http://www.jetbrains.com/resharper]( http://www.jetbrains.com/resharper)|
