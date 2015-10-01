# TestingContext
A tool for advanced data search, designed for use with SpecFlow.

# Historical reasons
Most of the time, when you test something, it makes sense to create test data for your testcase.
Here is the example of how it usually works. You write a SpecFlow scenario like this
```Cucumber
Given that i have a policy created in year 2006
  And policy has a coverage with type 'Dependent' and over 70 people covered
```
In the backend, you create a code like this
```C#
policy = new Policy { Created = new DateTime(2006, 1, 2), Coverages = new List<Coverage>() };
policy.Coverages.Add(new Coverage { Type = coverageType, HeadCount = headCount + 1 });
```
First line of such a code goes to the definition of a first SpecFlow step and second line goes for definition of a second step. Of course there are some means to reach that policy object and share it between steps, i am not mentioning any of that here to not flood the article with details, trying to show only meaningful parts of code.
Anyway. Sometimes, it so happens that you need to test an application, web portal for example and it has a lot of data consumed and displayed on the page. And, even though you only want to test small part of it, you will have to create a lot of data, so that other parts, displayed on html page would not fail because of no data, so to set up such a test you will need to do a lot of code and, which is even worse, a lot of lines in scenario file.

Another approach can be taken to test an application like that. This another approach would not work at first if you are working on application from the ground up, for obvious reasons, but it fits great if you already have a lot of functionality, and you are adding new, or modifying existing features. Approach is to have existing test data to choose from, using specified conditions. It can be a test database, or a set of 'mock' files, containing the data. You can reroute implementation of service interfaces from real db services to mocking services, loading and deserializing data from such a file.
So, to test the feature, you would still need policy from year 2006 that has 70 dependents covered, but you will not create the policy, and instead you choose from policies that you already have in database. Any of which has a lot of other data filled in, besides coverage that you need. Taxes, annual premiums, renewal proposals and so on, the model in our application was using over 20 db tables. Here to demonstrate the concept, I am using very simplified model which does not have all that extra child entities. Let's just say each policy has a lot of data that you don't need to create, check, or even be aware of its existense, because it is not needed for the test at hand.
The C# step definitions gets changed to look like the following
```C#
policies = policies.Where(x => x.Created.Year == year);
policies = policies.Where(x => x.Coverages.Any(y => y.Type == coverageType && y.HeadCount > headCount));
```
Of course you will need to define policies first, in pre-scenario hook, or in explicit extra step definition, which i personally think is much better, explicit over implied and all.
After these steps, you create a step that opens a portal with policyId parameter fit into http request line, with the id taken from first policy in policies enumerable, and then test what you intended to in subsequent steps.

# Advanced search
So you did find a policy that you need, now you are testing the UI. You checked that policy name is displayed in some section on the ui, now you need to check that some UI section displays that seventy-something number of dependents. And there you hit a problem. You have a reference to a policy, but how would you know which of, let's say, five coverage lines did trigger the predicate for the policy? Which coverage would you take to compare its headcount to the number displayed on UI? 
You could specify the coverage condition the second time in "Then" scenario step but that leads to a duplication. So we needed a tool to help avoid such a duplication
The tool to specify coverage condition separated from policy and allow to retrieve both policy and coverage. And any other entity in the model tree if we want to specify more than two entities.
Another benefit is that it will also allows to split two conditions we have for coverage into two separate step definitions.
Like the following
```Cucumber
	Given policy A is taken from policiesSource   #1
	  And policy A is created in year 2007        #2
	  And for policy A exists a coverage A        #3
	  And coverage A has type 'Dependent'         #4
	  And coverage A has over 70 people covered   #5
```
I marked steps with numbers here, only to show which step has which definition, here in readme. These numbers are not present in actual code.
For step definitions i use (TestingContext context) injected into a constructor, and used in step definitions of this sample.
Syntax for step definitions is the following
```C#
#1
context.Register().Provide<Policy>(key, x => PoliciesSource.Policies);
#2
context.For<Entities.Policy>(key).Filter(policy => policy.Created.Year == year); 
#3
context.Register()
    .DependsOn<Policy>(policyKey)
    .Provide<Coverage>(coverageKey, policy => policy.Coverages)
    .Exists();
#4
context.For<Coverage>(key).Filter(coverage => coverage.Type == type);
#5
context.For<Coverage>(key).Filter(coverage => coverage.HeadCount > headCount);
```
Notice that you can interchange any of these five lines in the scenario, i.e. put them in any order. That could be useful if you are planning to use SpecFlow "Background"
To get the search results, following syntax can be used
```C#
var policy = context.Value<Policy>(policyKey);
var policies = context.All<Policy>(policyKey);
var coverages = context.All<Coverage>(coverageKey);
```
First line will return first policy that meets all the conditions. I.e. policy that is created in year 2007 and has a Dependent coverage with over 70 people covered. All the filters are combined using "AND" logic.
Second line will return all the policies that meets these conditions.
Third line returns all the coverages that have type 'Dependent' and over 70 people covered in all policies that match the condition. I.e. result will not contain such a coverage that was inside policy created in some other year.
Notice that "All" method returns IEnumerable<IResolutionContext<Policy>> and not IEnumerable<Policy>. so, to get the latter, you need to do a Select of Value.
IResolutionContext will allow to get needed coverages of a specific policy, using the following syntax
```C#
var policies = context.All<Policy>(key);
var firstPolicyCoverages = policies.First().Get<Coverage>(key);
```

