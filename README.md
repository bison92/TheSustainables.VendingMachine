# TheSustainables.VendingMachine

[![Travis-CI build status](https://travis-ci.com/bison92/TheSustainables.VendingMachine.svg?branch=master)](https://travis-ci.com/bison92/TheSustainables.VendingMachine.svg?branch=master)

## Description
We would like you to create a model of virtual [vending machine](https://en.wikipedia.org/wiki/Vending_machine).
Complex UI is not required, just make sure that the requirements are covered. 
Also would be beneficial to briefly describe what you would do differently if you had more time.

In the case of a missing requirement please come up with a reasonable solution and document your decision.

## Initial data

Vending machine contains the following products:
* Tea (1.30 eur), 10 portions
* Espresso (1.80 eur), 20 portions
* Juice  (1.80 eur), 20 portions
* Chicken soup (1.80 eur), 15 portions

At the start the vending machine wallet contains the following coins (for exchange):
* 10 cent, 100 coins
* 20 cent, 100 coins
* 50 cent, 100 coins
* 1 euro, 100 coins

The customer has an unlimited supply of coins.

## Vending machine should support the following features:

*Accept coins* Customer should be able to insert coins to the vending machine.

*Return coins* Customer should be able to take the back the inserted coins in case customer decides to cancel his purchase.

*Sell a product* Customer should be able to buy a product:
* If the product price is less than the deposited amount, Vending machine should show a message “Thank you” and return the difference between the inserted amount and the price using the smallest number of coins possible.
* If the product price is higher than the amount inserted, Vending machine should show a message “Insufficient amount”
The amount and type of coins returned should be shown by the UI.

## Third Party Libraries 

* Serilog AspNetCore [(github)](https://github.com/serilog/serilog-aspnetcore)
* NSwag [(github)](https://github.com/RicoSuter/NSwag)


## Comments

Backend works in cents so 3.20€ its 320 cents in backend.

Sell operation is executed only if there's enough stock and enough change.

I've also added errors for "Not enough Stock" and "Not enough change"

If I've got more time I would like to add some more tests, specially the Sell method and the frontend code. Also the UI is quite ugly, but functional (and responsive).