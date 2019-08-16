Steps:
- Initialized new Angular Dot.NET Core 2.2 app
- Delete client app and recreated an new Angular app using last version of Angular
- Edit Dockerfile to make it compile
	- Install Nodejs and NPM
	- Reinstall @angular/cli to fix path bug during `dotnet publish ...`
- set `"outputPath": "dist"` in `angular.json`