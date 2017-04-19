Feature: SearchTagFeature
	To verify search tag functionality


Scenario: User is able to create search tag
	Given I login to Leads
	When I apply filters
	And I create a saved search
	Then I apply search tag to filter
