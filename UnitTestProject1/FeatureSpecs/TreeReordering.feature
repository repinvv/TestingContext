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
Scenario: Two levels tree reorder
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
