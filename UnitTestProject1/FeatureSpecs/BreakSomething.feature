Feature: BreakSomething
	In order to employ advanced testing technique
	As a test writer
	I want to be able to define filters first, then invert them afterwards

Background: 
   Given policy B is taken from policiesSource
	  And policy B is created in year 2011 
	  #CoverageExists
	  And for policy B exists a coverage B	 
	  And coverage B has type 'Dependent'
	  And coverage B has at least 20 people covered
	  #TaxExists 
	  And for policy B exists a tax B	 
	  And tax B has type 'Federal'
	  And tax B amounts to at least 70$

@breakOne1
Scenario: Happy path before breaking something
	Then policy B name must contain '@breakOne1'

@breakOne2
Scenario: No coverage with needed count and type
    Given condition 'CoverageExists' is broken
	Then policy B name must contain '@breakOne2'

@breakOne3
Scenario: No tax with needed amount and type
    Given condition 'TaxExists' is broken
	Then policy B name must contain '@breakOne3'