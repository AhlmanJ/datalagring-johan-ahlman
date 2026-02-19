# EducationPlatform

Hi!

I'm studying to become a .NET Developer at EC Utbildning and this is my fourth school assignment. This assignment was done for the course "Data Storage". The main focus of this assignment was to carry out modeling, normalization and implementation of a relational database. However, in order to be able to test the backend part which was the main focus of the assignment, i would also create a frontend that integrated with the backend through the Endpoints that i had created in my minimalAPI.

<u>This repository represents the backend part of the assignment and to test the application you also need to: </u>
- Use the GitHub repository "datalagring-johan-ahlman-frontend" which represents the frontend part of the application.
- Download and install a database. I used Microsoft SQL Server 2025 Express along with SQL Server Management Studio while developing this application. 
- Add the database to the solution in Visual Studio.
- Run both the server, Visual Studio solution in Visual Studio and the frontend in Visual Studio Code.

<u>Use of AI/chatGPT in the learning-process?</u>
- During the development of this application, in addition to following the lectures and teacher-led lessons at school, i have searched for information online for code examples and explanations about how to write code and why.
- <u>But to clarify how i use chatGPT in the learning process, i want to include this paragraph in my ReadMe.</u>
- In my solution, i have written comments in my files in the various projects about where i have used AI/chatGPT.
- If i haven't found good information on the internet about how to write a code or if i don't know at all what a code should look like, i have asked AI/chatGPT to either give me a code example of how to do it and then asked AI to explain each part of the code. Or if i    have found a code that i can use on the internet or in a lecture, i have asked AI to explain the parts of the code that have been unclear to me or that i haven't understood at all.
- By using AI in this way, i find i learn more and gain a better understanding of how to write code and why.
- So to sum this up, i use AI as an extra "teacher" who can explain code and give examples, just like our teacher does in our teacher-led lessons and i can ask the same question several times until i really understand.

-<u>NOTE! The frontend interface is coded to integrate with the backend via localhost:7253 , so you need to run the backend on localhost:7253</u>

<u>How to install and test this application?</u>
- Clone the repository to Visual Studio.
- Download and install a server. I have been using Microsoft SQL Server 2025 Express along with SQL Server Management Studio which can be downloaded from Microsoft's website.
- You will now have to add the server/database to the backend solution in Visual Studio:
- In Visual Studio, klick on "View" and choose "SQL Server Object Explorer".
- In the "SQL Server Object Explorer", click on "Add SQL Server" and select your server and it should now be connected to your solution.
- The next step is to migrate the entities to the database (tables).
- In Visual Stuido, klick on "Tools" and select "NuGet Package Manager" and select "Package Manager Console".
- In the console, insert text to the console and run: Add-Migration "MigrationToDatabase"
- If migration is "OK", insert text to the console and run: Update-Database
- Open SQL Server Management Studio and check tables in your server. They should now reflect the entities which you have in the solution in Visual Studio.
- You should now be able to test the solution togheter with the frontend if you run the server through SQL Server 2025 Configuration Manager, start/run the solution in Visual Studio and run the frontend in Visual Studio Code.

 - NOTE!
 - If you only want to test the Endpoints, you can run the server and start/run the backend solution in Visual Studio. When starting the solution, a console/termianl will be started were you can select a localhost.
 - hold "ctrl" + left click on: localhost:7253
 - A web page will now open.
 - Add: /swagger/index.html to the URL.
 - You should now be able to see all the endponts and test them.

<u>Some topics covered in this assignment:</u>
- C#
- ASP.NET Core Minimal API
- Modeling, normalization and implementation of a relational database
- Domain-Driven-Design and Clean Architecture
- CRUD (Create-Read-Update-Delete)
- Entity Framework Core and Code First-principle
- Unit tests with xUnit and NSubstitute
- Microsoft SQL Server
- Relational databases
- ORM (Object-Relational Mapping)
- Unit of work
- Global Expression Handler
