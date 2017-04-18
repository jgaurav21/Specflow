Feature: LoginFeature
	Verify the login functionality

Scenario: User is able to login
	Given I launch the browser
	When I enter username
	And I enter password
	And I click login button
	Then I logged into application
