Feature: Buy refrigerator

the user must purchase a refrigerator of different brands 
and verify if the quantity is less or more than the quantity specified in each test and each purchase.

@Test1
Scenario: Buy a Mabe refrigerator and verify if the amount is less than 10000
	Given Open Browser
	When Search a refrigerator & Add to the cart a mabe refrigerator without protection
	Then Verify if the refrigerator is added to the cart correctly
	
@Test2
Scenario: Buy a Samsung refrigerator and verify if the amount is greather than 10000
	Given Open Browser
	When Search a refrigerator & Add to the cart a mabe refrigerator without protection
	Then Verify if the Samsung refrigerator is added to the cart correctly
