# Proact

Proact is a server-side web framework...

Wait... Don't go...

Yes, I know what you are thinking. 

"_A server-side web framework!? That is not cool and totally outdated. Javascript frontend frameworks and SPAs are way more fun and hype_"

You are correct! Javascript frontend frameworks are fun and offer an awesome developer experience.

The purpose of Proact is to bring some of the frontend innovation and developer experience to the server. 

And more controversially: **Kill the hype Single Page Application hype**

### Getting started

Nuget package

``C#
// Code snippet
``


### Why do we want to kill the Single Page Application (SPA) hype

From a frontend perspective, SPAs are fun, but they come with a cost for the entire application. The application is divided in two: a frontend and a backend, which adds quite a lot of complexity. 

The clear evidence of the complexity, is that it usually requires the combined skills of two developers (a frontend and backend developer) to create a webapp. This just seems a bit too difficult.

We can also illustrate the complexity, if we image a Todo-list application. 
A SPA framework is often illustrated from this. In isolation, the frontend is fun and easy to develop. 
We can easily store the todo list in the client and get, add, edit and remove todo-items. 
This creates a great illusion of a web application, but it is not a real web application.

You will need to overcome a long list of hurdles and complexities before you are hitting production.

- The SEO performance is affected by a SPA. A real customer facing application needs to top google searches and links should be shareable on social media. This is not a feature of a SPA, and you need to somehow prerender a SPA to make it crawlable by internet bots
- The infrastructure of SPA development is complicated, but because you need to deploy both a frontend and a backend in two different ways. 
- An API between the frontend and the backend is required. The implementation will take time, but also defining and agreeing on the shape of the API will take time. Furthermore, you will need to export the API and exchange to the frontend
- Changes affecting the API are hard, because stuff can easily break
- SPAs are less secure and the security needs to be handled both in the frontend and in the backend
- State management is hard
- Routing is hard in SPAs

All the issues above are solvable, but the solutions often seem overengineered.  


