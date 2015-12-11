Feature: Forbid reordering in case when order of entities is not explicitly specified
	In order to avoid resolution uncertainty
	I want to get the exception mentoning items for which the order is not explicitly specified

@reorderingLimitation
Scenario: Two "Each" filters with simple reordering
   Given insurance B is taken from insurancesSource
     And insurance B is created in year 2018
	 And all assignments B in insurance B meet following criteria
	 And all taxes B in insurance B meet following criteria
	 And assignment B is created at the same day as tax B
    When i try resolving insurance B   
    Then i should get an exception with information about Assignment "B"
     And i should get an exception with information about Tax "B" 