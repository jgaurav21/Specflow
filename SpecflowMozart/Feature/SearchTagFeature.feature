Feature: SearchTagFeature
		To verify search tag functionality
@searchTag
Scenario: TC_001 User is able to create search tag 
	Given I login to Leads
	When I apply filters for search tag
	Then I create a search with search tag
	Then I verify search tag on Manage Searches page

@searchTag
Scenario: TC_002 User is able to edit search tag from saved search pop up
	Given I login to Leads
	When I create 1 search tag
	And I edit the filters
	Then I edit search tag

@searchTag
Scenario: TC_003 User is able to edit search tag from Manage searches page
	Given I login to Leads
	When I create 1 search tag
	And I navigate to Manage Searches page
	And I edit search tag
	Then I verify search tag on Saved Search pop up.

@searchTag
Scenario: TC_004 Validate search tag on Search Result page and Details page
	Given I login to Leads
	When I create 2 search tag
	And I apply search tag as filter
	| option              |
	| At least one search tag |
	| all search tags |
	Then I see search tag on each result
	When I navigate to project details page
	Then I verify search tag project details page

@searchTag
Scenario: TC_005 Validate search tag on Manage Searches page
	Given I login to Leads
	When I create 1 search tag
	And I navigate to Manage Searches page
	And I change email alert and verify search tag
	And I change CRM export and Verify search tag
	And I change display on snapshot and verify search tag

	@searchTag
	Scenario:  TC_006 Validate delete search tag
	Given I login to Leads
	When I create 2 search tag
	And I navigate to save search pop up
	And I delete search tag
	Then I get delete validation message
	And I navigate to Manage Searches Page
	And I delete search tag
	Then I get delete validation message