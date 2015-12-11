Feature: Registrations loop detection
	In order to understand why registrations do not work	
	I want to be presented with meaningful exception at registration time
	instead of "stack overflow" at execution time.

@loopDetection
Scenario: Loop Detection 1
	Given insurance B is taken from insurancesSource
	  And assignment B has more people than assignments C
	  And assignment C has more people than assignments D
	  And for insurance B exists an assignment B
	  And for insurance B exists an assignment C
	  And for insurance B exists an assignment D
	  And assignment D has more people than assignments C
	When i try resolving insurance B
	Then i should get an exception with information about Assignment "D"
