Feature: Google

Scenario: Google search
Given I am on google
When I enter the search term 'banana' and submit the search
Then I should be navigated to the results page
And the result should contain 'banana'