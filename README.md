# ddui

## Project setup
```
npm install
```

### Compiles and hot-reloads for development
```
npm run serve
```

### Compiles and minifies for production
```
npm run build
```

### Lints and fixes files
```
npm run lint
```

### Customize configuration
See [Configuration Reference](https://cli.vuejs.org/config/).

### Re-generate the Client Code

If the file is missing or not generated, try running dotnet-svcutil again:

dotnet-svcutil http://localhost:5097/LookupService.svc?wsdl

add Reference.cs to your file


### dotnet add package System.Data.SqlClient --version 4.8.3
   using System.Data.SqlClient;

### dotnet add package Microsoft.Data.SqlClient --version 5.0.0
   using Microsoft.Data.SqlClient;
