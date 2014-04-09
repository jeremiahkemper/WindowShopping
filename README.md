WindowShopping
==============

Tools for communicating to the Windows Store APIs.  Contains an abstraction layer for the communication, and several XAML Behaviors. Written in C# for WinRT.

## Why use it?
Talking to the store is basically boiler plate code for most applications.  Creating an abstract layer to determine whether or not to use the `CurrentAppSimulator`, or the `CurrentApp` objects is time consuming.  This library takes the guess work out of the abstractions, and allows the user to do standard tasks without any code using behaviors and live, bindable lists.


## What's in it?

### DLL's
* WindowShopping.dll - home for core functionality, and the abstraction layer to communicate to the store API's
* WindowShopping.UI.dll - home for UI functionality, ViewModels, and Behaviors
 
### Highlights
* `StoreContainer` - the static class containing the active store API repository
* `CurrentAppRepository` - a repository which does all of the I/O between the store and the application
* `CurrentAppSimulatorRepository` - found in the sample application, this allows debugging several store scenarios when building the application
* `ConsumableFulfillmentAction` - tells the Store when a consumable in app purchase purchased product has been fulfilled
* `ProductPurchaseAction` - performs an in app purchase for a specified product
* `ProductStatusBehavior` - performs actions based on the purchase status of a product
* `PurchaseFullAppAction` - performs a purchase for the full version of the application
* `RateApplicationAction` - sends the user to the store to rate the application
* `TrialModeBehavior` - performs actions based on the purchase status of the application
* `LiveAppInfo` - a bindable class which keeps track of the status of the application
* `LiveConsumablesListing` - a bindable list which keeps track of the Unfulfilled Consumables
* `LiveProductListing` - a bindable list which keeps track of the products available to purchase

## Samples
Included in this repo is `WindowShopping.Samples.sln` which contains a Windows application using all of the behaviors and live classes. It also listens to the broadcasting log from the active abstraction layer.  

The sample overrides the default `CurrentApp` object with the `CurrentAppSimulator` during debugging by using the `CurrentAppSimulatorRepository`.
