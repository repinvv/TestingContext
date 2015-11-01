Feature: BreakSomething
	In order to employ advanced testing technique
	As a test writer
	I want to be able to define filters first, then invert them afterwards

Background: 
   Given insurance B is taken from policiesSource
	  And insurance B is created in year 2011 	  
	  And for insurance B exists an assignment B	 
	  And assignment B has type 'Dependent'
	  And assignment B has at least 20 people covered	  
	  And for insurance B exists a tax B	 
	  And tax B has type 'Federal'
	  And tax B amounts to at least 70$

@breakOne1
Scenario: Happy path before breaking something
	Then insurance B name must contain '@breakOne1'

@breakOne2
Scenario: No assignment with needed count and type
    Given there is no suitable assignment B
	Then insurance B name must contain '@breakOne2'

@breakOne3
Scenario: No tax with needed amount and type
    Given there is no suitable tax B
	Then insurance B name must contain '@breakOne3'