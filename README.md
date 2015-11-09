Bugur
=====
A simple image hosting application for a local network.
Built on ASP.Net 5 beta8.

Security isn't a goal of this project, in particular:
* there's no authentication,
* detailed server errors are reported.

Demo
----
![Bugur demo](http://i.imgur.com/kN2rMki.gif "Demo")

Prerequisites
-------------
DNVM, see http://docs.asp.net/en/latest/getting-started/installing-on-windows.html

How to run
----------
1. Publish the project. The result is in the path_to_artifacts\bin\bugur\Release\PublishOutput directory.
2. Create the symbolic link:
  ```
  mklink /D path_to_artifacts\bin\bugur\Release\PublishOutput\approot\src\bugur\wwwroot path_to_artifacts\bin\bugur\Release\PublishOutput\wwwroot
  ```
  This is needed because appEnvironment.ApplicationBasePath is resolved differently when running
  from Visual Studio and as a published application.

3. **IIS**
  * Install httpPlatformHandler.
  * Create a site pointing to path_to_artifacts\bin\bugur\Release\PublishOutput\wwwroot
  
  -- OR --

  **Command line**

  Run path_to_artifacts\bin\bugur\Release\PublishOutput\approot\web.cmd



