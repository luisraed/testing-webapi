Feature: Inventory Update
	As a registered user of an inventory management system,
	I can update a product's quantity
	to correctly reflect the physical inventory of the product

Scenario: Products can be successfully added to the inventory
	Given an Inventory Update Request for an existing product
	And request is to add "5" products to the inventory
	When the Inventory Update Api is called
	Then the quantity of products should be increased by "5"

Scenario: Products can be successfully substracted from the inventory
	Given an Inventory Update Request for an existing product
	And request is to substract one product from the inventory
	When the Inventory Update Api is called
	Then the quantity of products should be decreased by "1"

Scenario: It is not allowed to substract more products than the existing in the inventory
	Given an Inventory Update Request for an existing product
	And request is to substract more products than the existing
	When the Inventory Update Api is called
	Then the quantity of products should not change