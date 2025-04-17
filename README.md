# SampleUserApp

This project is a full-stack web application built using ASP.NET Core Web API and Angular 17 (Vite). It allows administrators to manage a list of users with CRUD operations. Each user has associated details such as name, email, address, phone number, and institutional affiliation.

The backend is powered by Entity Framework Core, managing models like User, Address, Telephone, and UserInstitution. The frontend provides a responsive interface where users can view, add, or edit user data via a single reusable form component. The system routes users to the edit view for both creating and updating entries, improving code reusability and simplicity.

This app demonstrates a clean separation of concerns, practical usage of Angular routing, and integration with RESTful services in a real-world admin dashboard layout.
