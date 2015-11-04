Feature: TreeReordering
	In order to find all the combinations of data entities	
	I want a tree to be reordered when a filter between two entities is specified

@treeReordering1
Scenario: Simple tree reorder
	Given insurance B is taken from policiesSource
	  And insurance B is created in year 2012
	  And for insurance B exists an assignment B
	  And assignment B has type 'Dependent'
	  And for insurance B exists a tax B
	  And tax B has type 'Federal'	  
	  And assignment B is created at the same day as tax B
	Then insurance B name must contain '@treeReordering1'
	  And assignment B Id must be 12

@treeReordering2
Scenario: Two level tree reorder
	Given insurance B is taken from policiesSource
	  And insurance B is created in year 2012	  
	  And for insurance B exists an assignment B
	  And assignment B has type 'Dependent'
	  And insurance C is taken from policiesSource
	  And insurance C is created in year 2013
	  And for insurance C exists an assignment C
	  And assignment C has type 'Employee'
	  And assignment B is created at the same day as assignment C
	Then insurance B name must contain '@treeReordering1'
	  And insurance C name must contain '@treeReordering2'
	  And assignment B Id must be 12
	  And assignment C Id must be 15

@treeReordering3
Scenario: Two level tree reorder complexResolve
	Given insurance B is taken from policiesSource
	  And insurance B is created in year 2013	  
	  And for insurance B exists an assignment B
	  And assignment B has type 'Dependent'
	  And insurance C is taken from policiesSource
	  And insurance C is created in year 2014
	  And for insurance C exists an assignment C
	  And assignment C has type 'Employee'
	  And assignment B is created at the same day as assignment C
	Then insurance B name must contain '@treeReordering3 first'
	  And insurance C name must contain '@treeReordering3 second'
	  And for assignment C with id 24 there must be provided assignments B with ids 21,22
	  And for assignment B with id 20 there must be provided assignments C with ids 23,25	  
	  	  
@treeReordering4
Scenario: Simple tree reorder with collection filter
	Given insurance B is taken from policiesSource
	  And insurance B is created in year 2012
	  And for insurance B exists an assignment B
	  And assignment B has type 'Employee'
	  And for insurance B exists a tax B
	  And tax B has type 'Federal'	  
	  And assignment B is created at the same day as tax B
	  And assignments B cover 100 people total
	  And taxes B have total amount of 100$
	Then insurance B name must contain '@treeReordering4'	  

@treeReordering5
Scenario: Both singular and collection filters are assigned between branches
	Given insurance B is taken from policiesSource
	  And insurance B is created in year 2015
	  And for insurance B exists an assignment B
	  And assignment B has type 'Dependent'
	  And for insurance B exists a tax B
	  And tax B has type 'Federal'	  
	  And assignment B is created at the same day as tax B
	  And average payment per person in assignments B, specified in taxes B is over 10$
	Then insurance B name must contain '@treeReordering5'