# Contacts App (ASP.NET MVC)

An ASP.NET MVC application made in the spirit of the [official Flask example][htmx-proj] application 
that accompanies the book [Hypermedia Systems][htmx-book]. You can use this application as a starting point
to follow along with the book starting at [Chapter 05](https://hypermedia.systems/htmx-patterns/).

## Note

- I deliberately removed jQuery and jQuery-Validate to show a "chunky" Web 1.0 app with as little client-side
    JavaScript as possible. This is also more in line with the official [Flask][flask] [application][htmx-proj] used in 
    the [book][htmx-book].
- This project uses the [Library Manager][libman] [CLI][libman-cli] to manage client-side libraries. You do not need it,
    but I find since I have .NET on my system it is really handy for handling non-bundled client-side assets.

  ```shell
  # Install the client-side libraries
  cd HTMX.Web
  libman restore
  ```
- The data service is rudimentary as that is not the point of the book, and by using a JSON file
    as the data store, it is easy to follow along without setting up a database and worrying about additional dependencies.
- I will win no awards for design in my lifetime and I am okay with this.


[htmx]: https://htmx.org "High power tools for HTML"
[htmx-book]: https://hypermedia.systems/ "Hypermedia Systems Book"
[flask]: https://flask.palletsprojects.com/ "Flask - A minimal web framework for Python"
[htmx-proj]: https://github.com/bigskysoftware/contact-app "Contact App - official"
[libman]: https://devblogs.microsoft.com/dotnet/library-manager-client-side-content-manager-for-web-apps/ "Client-side content manager for web apps"
[libman-cli]: https://learn.microsoft.com/en-us/aspnet/core/client-side/libman/libman-cli
