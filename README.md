TODOs:
1) Move away from Javascript Maps Api to AzureMaps.
-   Reasoning(having the google maps api key in Host.cshtml is insecure, Azure maps also means AzureKeyValut, which means different keys for different environments in the future)

2) Move away from INotifyPropertyChanged, or at least create some base classes that handle Handler cleanup gracefully. 
-   Reasoning(It's a clunky pattern in it's current state, bot doing so would lead to alot of boilerplated code in the Dispose methods of ViewModels)

3) Create a proper custom Validator for DateTime
-   Reasoning(It's gross at the moment, creating one which is more inline with the pattern used in GlobalCoordinateValidator would be nicer(

4) Add BUnit tests to ensure the correct components are displayed for given states.
5) Add Integration tests using Selenium for cross browser and critical path functionality
6) Add handling of non 200 responses to the CrimeDataApi
7) Add Unit tests for the models.
8) Add Unit tests for DTO's expected deserialzed state using  using ApprovalTests alongside WinMerge
