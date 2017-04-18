Feature: LoginFeature
	Verify the login functionality

Scenario: User is able to login
	Given I launch the application
	When I enter <username> into username
	| username               |
	| gauravj@xpanxion.co.in |
	And I enter <password> into password
	| password |
	| Test1234 |
	And I click login button
	Then I logged into application
