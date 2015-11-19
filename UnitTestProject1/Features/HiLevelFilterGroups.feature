Feature: HiLevelFilterGroups
	In order to write high level tests
	As a test writer
	I want to specify OR and NOT-AND groups

@ORgroup
Scenario: OrGroup
    Given insurance B is taken from insurancesSource
      And insurance B is created in year 2016
	  And insuranse B matches high level OR condition
	Then insurances B must have names containing '@ORgroup1,@ORgroup2,@ORgroup3,@ORgroup4'

@NOTgroup
Scenario: NotAndGroup
    Given insurance B is taken from insurancesSource
      And insurance B is created in year 2017
	  And insuranse B matches high level NOT condition
	Then insurances B must have names containing '@NOTgroup1,@NOTgroup2,@NOTgroup3,@NOTgroup4'