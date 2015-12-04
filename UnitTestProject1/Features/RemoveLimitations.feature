Feature: RemoveLimitations
	In order to write the test the way i like
	I want to specify filters without limitations

@limitationRemoval2
Scenario: "Each" filter in scenario with tree reordering
   Given insurance B is taken from insurancesSource
     And insurance B is created in year 2018
	 And all assignments B in insurance B meet following criteria
	 And all taxes B in insurance B meet following criteria
	 And assignment B is created at the same day as tax B
   Then insurance B name must contain ' @limitationRemoval2'