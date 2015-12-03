@perftest
Feature: Performance test
	In order to know the performance limitations
	I want to be told the search time

Scenario Outline: Performance test
	Given I have a model with <branches> branches containing <items> items each		
	Then nothing should be found and the search time is under 1 sec
Examples: 
	| branches | items |
	| 5        | 5     |
	| 5        | 10    |
	| 5        | 14    |
	| 7        | 8     |
	| 100      | 100   | 
	#3k 100k 500k 2M
