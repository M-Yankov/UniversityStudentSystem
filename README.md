# UniversityStudentSystem
Students of the this system will have easy access to resources and information about education. 

## Introduction 

* For seeding data are used `Lorem.NET` and [https://randomuser.me/](https://randomuser.me/) for random user data, news and forum content.
* Technologies used
    * ASP.NET MVC 5
    * MS SQL Server (database)
* Components (packages)
    * Entity framework 6 - code first
    * Ninject - Dependency inversion
    * Automapper - binding models (database models to view models etc.)
    * Boostrap - for UI
    * Sb admin theme based on bootsrap - can be found here [http://startbootstrap.com/template-overviews/sb-admin/](http://startbootstrap.com/template-overviews/sb-admin/)
    * SinganlR - for the live chat
    * __Kendo UI MVC - If you want to run the project ensure you have installed Kendo UI MVC__  link: [Installation](http://docs.telerik.com/kendo-ui/aspnet-mvc/asp-net-mvc-5) 
    * Glimpse - statistics for the application (runs on admin accound only)
    * JQuery 

* Users: by default accounts are configured to have password equal to the username. 
    * Some examples:
        * _username:_ admin _pass:_ admin
        * _username:_ beautifulbird746 _pass:_ beautifulbird746

## Start
* When application is ready. You should see this home page:


## The Life cycle of a student

* Each student can apply a candidature for a specialty. Wait a approvement and can check your favorite courses. 
* Each course has task and tests. Solve them get good marks. 
* You can check your marks in the profile page.
* For solving the tasks for the course you should wrap all solutions in a single .zip archive less than 2Mb.
* If you want to get certificate you should study hard.
* Check out forum. You can make discussion with other students.
* If you found a bug in the system just go to the report page using the navigation menu.
* Follow our news about the university in the "News" section.
	
## Admins and trainers
	
* They should approve student candidatures.
* They can make a new courses.
* It's obligatory to upload resources about each courses. Students should have a way to learn from somewhere. 
* They should make tests to check knowledge of the students. 
* And finally give certificates to students in the specialty. 
	

