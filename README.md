# TestingContext
A tool for advanced data search, designed for use with SpecFlow.

# Historical reasons
Most of the time, when you test something, it makes sense to create test data for your testcase.
Here is the example of how it usually works. You write a SpecFlow scenario like this
```Cucumber
Given that i have a policy created in year 2006
  And policy has a coverage with type 'Dependent' and over 70 people covered
```
Behind these steps you create definitions with code like this
```C#
policy = new Policy { Created = new DateTime(2006, 1, 2), Coverages = new List<Coverage>() };
policy.Coverages.Add(new Coverage { Type = coverageType, HeadCount = headCount + 1 });
```
First line of such a code goes to the definition of a first SpecFlow step and second line goes for definition of a second step respectively. Of course there are some means to reach that policy object and share it between steps, i am not mentioning any of that here to not flood the article with details, trying to show only meaningful parts of code.
Anyway. Sometimes, it so happens that you need to test an application, web portal for example, and it has a lot of data consumed and displayed on the page. And, even though you only want to test small part of it, you will have to create a lot of data, so that other parts, displayed on html page would not fail because of no data. So to set up such a test you will need to do a lot of code and, which is even worse, a lot of lines in scenario file.

Another approach can be taken to test an application like that. This another approach would not work at first if you are working on application from the ground up, for obvious reasons, but it fits great if you already have a lot of functionality, and you are adding new, or modifying existing features. Approach is to have existing test data to choose from, using specified conditions. It can be a test database, or a set of 'mock' files, containing the data.
So, to test the feature, you would still need a policy from year 2006 that has 70 dependents covered, but you will not create the policy, and instead you choose from policies that you already have in database. Any of these policies might have a lot of other data filled in, besides coverage that you need. Taxes, annual premiums, renewal proposals and so on, the model in our application was using over 20 db tables. Here to demonstrate the concept, I am using very simplified model which does not have all that extra child entities. Let's just say each policy has a lot of data that you don't need to create, check, or even be aware of its existense, because it is not needed for the test at hand.
The C# step definitions gets changed to look like the following
```C#
policies = policies.Where(x => x.Created.Year == year);
policies = policies.Where(x => x.Coverages.Any(y => y.Type == coverageType && y.HeadCount > headCount));
```
Of course you will need to define policies source first, in pre-scenario hook, or in explicit extra step definition, which i personally think is much better, explicit over implied and all.
After these steps, you create a step that opens up a portal with policyId parameter fit into http request line, with the id taken from first policy in policies enumerable, and then test what you intended to in subsequent steps.

# Advanced search
So you did find a policy that you need, now you are testing the UI. You checked that policy name is displayed in some section on the ui, now you need to check that some UI section displays that seventy-something number of dependents. And there you hit a problem. You have a reference to a policy, but how would you know which of, let's say, five coverage lines did trigger the predicate for the policy? Which coverage would you take to compare its headcount to the number displayed on UI? 
You could specify the coverage condition the second time in "Then" scenario step but that leads to a duplication. So we needed a tool to help avoid such a duplication.
The tool to specify coverage condition separated from policy and allow to retrieve both policy and coverage. And any other entity in the model tree if we want to specify more than two entities.
Another benefit this search engine gives is that it will also allows to split two conditions we have for coverage into two separate step definitions.
Like the following
```Cucumber
Given policy A is taken from policiesSource   #1
  And policy A is created in year 2007        #2
  And for policy A exists a coverage A        #3
  And coverage A has type 'Dependent'         #4
  And coverage A has over 70 people covered   #5
```
I marked steps with numbers here, only to show which step has which definition, here in readme. These numbers are not present in actual code.
For step definitions there is (TestingContext context) injected into a constructor, and used in step definitions.
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
Notice that you can interchange any of five lines in the scenario, i.e. put them all in any order. That could be useful if you are planning to use SpecFlow "Background" feature.

# Retrieving search results
To get the search results, following syntax can be used
```C#
var policy = context.Value<Policy>(policyKey);
var policies = context.All<Policy>(policyKey);
var coverages = context.All<Coverage>(coverageKey);
```
First line will return first policy that meets all the conditions. I.e. policy that is created in year 2007 and has a Dependent coverage with over 70 people covered. All the filters are combined using "AND" logic, the same as SpecFlow/Cucumber syntax tells us.
Second line will return all the policies that meet these conditions.
Third line returns all the coverages that have type 'Dependent' and over 70 people covered in all policies that match the condition. I.e. result will not contain such a coverage that was inside policy created in some other year.
Notice that "All" method returns IEnumerable\<IResolutionContext\<Policy\>\> and not IEnumerable\<Policy\>. so, to get the latter, you need to do a Select of Value.
IResolutionContext will allow to get needed coverages of a specific policy, using the following syntax
```C#
var policies = context.All<Policy>(policyKey);
var firstPolicyCoverages = policies.First().Get<Coverage>(coverageKey);
```

# Combined filters
For some cases you will need to compare fields of 2 entities. Here is the example:
```Cucumber
Given policy A is taken from policiesSource
  And for policy A exists a coverage A
  And coverage A has type 'Dependent' 
  And coverage A covers less people than maximum dependendts specified in policy A
```
Notice that 3 out of 4 steps are reused from previous tests. And this becomes a trend, i.e. when i was writing tests for the search functionality, i had over 75% reuse achieved while having as much as 5-6 tests behind me. This is the beauty of combining SpecFlow and granularity, achieved using the advanced search tool. 
Also, the condition looks quite synthetic, i did not come up with more lifelike condition for this model. 
Anyway, here is the step definition
```C#
context
    .For<Coverage>(coverageKey)
    .With<Policy>(policyKey)
    .Filter((coverage, policy) => coverage.HeadCount < policy.MaximumDependents);
```
There is currently no way to add third entity here, but that is ideological limitation, meaning that if you have a condition that uses 3 or more entities in it, most likely you can break it into smaller conditions which would use 2 entities. There is no technical limitation though, i could add the option for a 3-entity filter anytime.

# Collection filters
There are several predefined collection filters that can be used. One was used above
```C#
context.Register()
    .DependsOn<Policy>(policyKey)
    .Provide<Coverage>(coverageKey, policy => policy.Coverages)
    .Exists();
```
"Exists" filter means that policy meets condition if there is at least one coverage that meets all the conditions specified. There are also "DoesNotExist" and "Each" collection filters, with corresponding functions.
Also, there is a possibility to define custom collection filter. For example
```C#
context.ForCollection<Coverage>(key)
       .Filter(coverages => coverages.Sum(x => x.Value.HeadCount) > 0);
```
This filter means that policy meets condition if total headcount of coverages(with each of them meeting their own condition), is positive number.

# Branches
With 2 entities on hand, it is pretty much obvious what the resolved structure is - there is a collection of items, containing one policy each, and a child collection of items containing one coverage each.
When third entity comes into the test's scope, which is taken from coverage, the structure scales in pretty much obvious way, so i did not try and simulate such a condition here.
The difference arises when that third entity is a second child of a root element, i.e. policy.
There are 2 ways to register, and then to resolve such a case.
First, obvious way is the following
```C#
context.Register()
       .DependsOn<Policy>(policyKey)
       .Provide<Tax>(taxKey, policy => policy.Taxes)
       .Exists();
```
This way, the structure becomes a tree. For each policy there will be a collection of matching coverages and a second collection of matching taxes. 
However, if you want to iterate through all the combinations and compare each coverage to each tax(i will use another synthetic condition here to demonstrate), then tree structure is not good for you and you will need a chain structure, i.e. tree with a single branch. This can be done the following way
```C#
context.Register()
       .DependsOn<Coverage>(coverageKey)
	   .Resolves<Policy>(policyKey)
       .Provide<Tax>(taxKey, policy => policy.Taxes)
       .Exists();
	   
context.For<Tax>(taxKey)
       .With<Coverage>(coverageKey)
       .Filter((tax, coverage) => tax.Id == coverage.Id);
```
This way all the pairs of tax and coverage with matching id's will be found. However, if you use this filter on the tree structure, a tax will be valid if it's id is equal to id of the first coverage.

# Comparing two collections
Tree structure, allows to define a filter that uses 2 collections. It can't happen in the chain, because for every collection there is only a singular parent, and a singular parent of parent, there is no other collection that can be reached.
The example of comparing 2 collections
```Cucumber
  And average payment per person in coverages B, specified in taxes B is over 10$
```
```C#
context
    .ForCollection<Coverage>(coverageKey)
    .WithCollection<Tax>(taxKey)
    .Filter((coverages, taxes) => taxes.Sum(x => x.Value.Amount) / coverages.Sum(x => x.Value.HeadCount) > average);
```

# Break something
This testing technique has to be widely known, i am just do not have a correct name for it.
Anyway, the technique is to prepare a "happy path" case, check that it works, and then break one thing at a time to check the bad cases.
For example, for a happy path you can find and test the participant who has good password, email, access rights and so on.
You make sure that page for that participant works all right. Then you might want to test that this page does not work if person is missing anything.
For one test you get that "good" participant and break his password, for another test you break his email and so on. 
This does not test all the combinations of data, but i would not assume that page was designed specicically to cheat its way through "negative" tests.
<br/>
Anyway, here is the example that i created using my model
```Cucumber
Background: 
   Given policy B is taken from policiesSource
	  And for policy B exists a coverage B
	  And for policy B exists a tax B

Scenario: No coverage with needed count and type
    Given condition 'CoverageExists' is broken

Scenario: No tax with needed amount and type
    Given condition 'TaxExists' is broken
```
To implement such a behavior, any filter can be registered in background with a key, and then "inverted" in specific scenario.
So that for a happy path it will return "meets condition" true for a policy having any coverage(same for tax), and when you invert it, 
it will return true for policy that does not have any coverage.

# Logging a search failure
Sometimes, when a lot of conditions specified, it is not that obvious why search does not yield any results. For that case there is an option to display the filter that invalidated the search last.
I.e. if you have 3 filters, and first filter invalidates half the entities, second filter invalidates the other half, third filter will not even trigger. There is a way to display information about that second filter.
To do that, you have to implement IResolutionLog interface and assign the instance to ResolutionLog property of the context prior to the search.

# Limitations
1. Combined filter can only reference either singular parent in the same branch. Can not try to reference a collection of a parent type, because no collection is availabe in the chain.
2. Combined filter can not reference a child.
2. Circular dependencies are not allowed. If first branch references second branch, second branch is not allowed to reference the first branch
Search tool will throw a ResolutionException in case it finds one of these violations.

# Installation
Package is available on NuGet under the name TestingContext