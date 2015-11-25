Feature: RemoveLimitations
	In order to write the test the way i like
	I want to specify filters without limitations

@limitationRemoval1
Scenario: Combination filter between child entity and collection of parents
	Given insurance B is taken from insurancesSource
	And for insurance B exists an assignment B
	And assignment B has one item in insurances B. Do not pay any attention to how dumb it sounds
	And insurance C is taken from insurancesSource
	And for insurance C exists an assignment C	
	Then insurance B must exist
	And insurances B count must be equal to insurances C count
