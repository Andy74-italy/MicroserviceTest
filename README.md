# MicroserviceTest

This solution was born with the aim of deepening microservices in .NET, but as happens every time, I find myself optimizing the scenarios in order to minimize and simplify development.

About that, the first aspect of a microservice is the exposure of the REST API, for the offer and use of the services displayed.

## REST API

In the "**main**" branch there is the project developed respecting the guidelines and best practices for this type of system.
There are two further branches that evolve the original project, optimizing the development in two different directions. The first, called "**EntityInjection**", generalizes a system based on exposing *CRUD* functionality on standard entities, while the second, "***ControllerInjection***", aims to allow the implementation of modules that contain the entire controller to be exposed as an MVC component.

In both cases, a plug-in methodology is adopted, ie the ability to load components by providing libraries to the system core, thus enriching its functionality.

For details, refer to the specific readme files for each branch.