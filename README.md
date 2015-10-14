# TestingContext
A tool for advanced data search, designed for use with SpecFlow.

# Historical reasons
<h3>Classic approach</h3>
Most of the time, when you test something, it makes sense to create test data for your testcase. Here is the example of how it usually works. You write a SpecFlow scenario like this
```Cucumber
Given that I have a insurance created in year 2006
  And insurance has a assignment with type 'Dependent' and over 70 people covered
```
Behind these steps you create definitions with code like this
```C#
insurance = new Insurance { Created = new DateTime(2006, 1, 2), Assignments = new List<Assignment>() };
insurance.Assignments.Add(new Assignment { Type = assignmentType, HeadCount = headCount + 1 });
```
The first line of such a code goes to the definition of the first SpecFlow step and the second line goes to the definition of a second step respectively. Of course there are some means to reach that insurance object and share it between steps, I don't mention any of them here in order not to flood the article with details, and I show only meaningful parts of code.
Anyway, sometimes, it happens so that you need to test an application, a web portal for example, which has a lot of data consumed and displayed on the page. And, even though you only want to test a small part of it, you will have to create a lot of data, so that other parts, displayed on a particular html page would not fail because of data absense. So to set up such a test you will need to write a lot of code and, a lot of lines in the scenario file, which is even worse.

<h3>Alternative</h3>
Another approach can be taken to test an application like that. For obvious reasons, it will  not work at first if you develop the application from the ground up, for obvious reasons, but it fits perfectly if you already have a lot of functionality, and you are adding new, or modifying existing features. The approach is to use existing test data, selected using specified predicates. It can be a test database, or a set of 'mock' files, containing the data.
So, to test the feature, you still need a insurance created in 2006 that has 70 dependents covered, but you will not create a new insurance object, instead of this you choose one from the policies that you already have in the database. Any of these policies might contain a lot of other different data, such as taxes, annual premiums, renewal proposals and so on, the model in our application was using over 20 db tables. To demonstrate the concept, I use a very simplified model which does not have all extra child entities. Let's just say each insurance has a lot of data that you don't need to create, check, or even be aware of its existence, because it is not needed for the test at hand.
The C# step definitions gets changed as follows
```C#
policies = policies.Where(x => x.Created.Year == year);
policies = policies.Where(x => x.Assignments.Any(y => y.Type == assignmentType && y.HeadCount > headCount));
```
Of course you will need to define policies source first, in the pre-scenario hook, or in the explicit extra step definition, which I personally think is much better, as i prefer explicit over implied.
After these steps, you create a step that opens up a portal with the insurance ID parameter in HTTP request line, with the ID taken from the first insurance in policies enumerable. And then test what you intended to in subsequent steps.

# Advanced search
So you found the insurance that you need, now you are testing the UI. You checked that the insurance name is displayed in some section on the UI, now you need to check that appropriate UI section displays that seventy-something number of dependents. And there you hit a problem. You have a reference to a insurance, but how would you know which of, let's say, five assignment lines did trigger the predicate for the insurance? Which assignment would you take to compare its headcount to the number displayed on UI? 
You can specify the assignment condition in the separate predicate, placed in "Then" scenario step but that leads to duplication. So we needed the tool to help avoid such duplication.
The tool allowing to specify assignment condition without mentioning insurance in it. Allowing to retrieve both insurance and assignment, given that each condition is present in one predicate only. And any other entity in the model tree if we want to specify more than two entities.
Another benefit given by this search engine is that it will also allows splitting two conditions we have for assignment into two separate predicates and put them int two separate step definitions.
```Cucumber
Given insurance A is taken from policiesSource   #1
  And insurance A is created in year 2007        #2
  And for insurance A exists a assignment A        #3
  And assignment A has type 'Dependent'         #4
  And assignment A has over 70 people covered   #5
```
I marked steps with numbers here only to show which step has which definition. These numbers are not present in the actual code.
For step definitions there is (TestingContext context) injected into test classes constructors, and used in the step definitions.
Syntax for step definitions is the following
```C#
#1
context.Register().Provide<Insurance>(key, x => PoliciesSource.Policies);
#2
context.For<Entities.Insurance>(key).Filter(insurance => insurance.Created.Year == year); 
#3
context.Register()
    .DependsOn<Insurance>(insuranceKey)
    .Provide<Assignment>(assignmentKey, insurance => insurance.Assignments)
    .Exists();
#4
context.For<Assignment>(key).Filter(assignment => assignment.Type == type);
#5
context.For<Assignment>(key).Filter(assignment => assignment.HeadCount > headCount);
```
Please note that you can interchange any of five lines in the scenario, i.e. put them all in any order. That could be useful if you are planning to use SpecFlow "Background" feature.

# Retrieving search results
You can use the following syntax to get the search results
```C#
var insurance = context.Value<Insurance>(insuranceKey);
var policies = context.All<Insurance>(insuranceKey);
var assignments = context.All<Assignment>(assignmentKey);
```
The first line will return the first insurance that meets all the conditions. I.e. insurance created in 2007, that has a Dependent assignment with over 70 people covered. All the filters are combined using "AND" logic, the same as SpecFlow/Cucumber syntax tells us.
The second line will return all the policies that meet these conditions.
The third line returns all the assignments that have 'Dependent' type and over 70 people covered in all the policies that match the condition. I.e. result will not contain such a assignment that was inside the insurance created in some other year.
Note that "All" method returns IEnumerable\<IResolutionContext\<Insurance\>\> and not IEnumerable\<Insurance\>. So, to get the latter, you need to do a Select of Value.
IResolutionContext will allow to get needed assignments of a specific insurance, using the following syntax
```C#
var policies = context.All<Insurance>(insuranceKey);
var firstInsuranceAssignments = policies.First().Get<Assignment>(assignmentKey);
```

# Combined filters
For some cases you will need to compare the fields of 2 entities. Here is the example:
```Cucumber
Given insurance A is taken from policiesSource
  And for insurance A exists a assignment A
  And assignment A has type 'Dependent' 
  And assignment A covers less people than maximum dependendts specified in insurance A
```
Note that 3 out of 4 steps are reused from the previous tests. And this becomes a trend, i.e. when I was writing tests for the search functionality, I had over 75% reuse ratio achieved while having as much as 5-6 tests behind me. This is the beauty of combining SpecFlow and granularity, achieved using the advanced search tool. 
Also, the condition looks quite synthetic, I did not come up with more lifelike condition for this model. 
Anyway, here is the step definition:
```C#
context
    .For<Assignment>(assignmentKey)
    .With<Insurance>(insuranceKey)
    .Filter((assignment, insurance) => assignment.HeadCount < insurance.MaximumDependents);
```
There is currently no way to add the third entity here, but that is an ideological limitation, meaning that if you have a condition that uses 3 or more entities in it, you can most likely break it into smaller conditions which would use 2 entities. There is no technical limitation though, I could add the option for a 3-entity filter anytime.

# Collection filters
There are several predefined collection filters that can be used. One of them was used above
```C#
context.Register()
    .DependsOn<Insurance>(insuranceKey)
    .Provide<Assignment>(assignmentKey, insurance => insurance.Assignments)
    .Exists();
```
"Exists" filter means that a insurance meets the condition if there is at least one assignment that meets all the conditions specified. There are also "DoesNotExist" and "Each" collection filters, with corresponding functions.
Also, there is a possibility to define custom collection filter. For example
```C#
context.ForCollection<Assignment>(key)
       .Filter(assignments => assignments.Sum(x => x.Value.HeadCount) > 0);
```
This filter means that a insurance meets the conditions if total headcount of assignments(with each of them meeting their own conditions), is positive number. Insurance is not mentioned here, but it is implied as previously assignment was registered to depend on it, so this child condition affects the current insurance as well.

# Branches
With 2 entities on hand, it is pretty much obvious what the resolved structure is - there is a collection of items, each containing one insurance, and a child collection of items each containing one assignment.
When the third entity comes into the test's scope, which is taken from assignment, the structure scales in pretty much obvious way, so I did not try and simulate such a condition here.
The difference arises when that third entity is a second child of a root element, i.e. the insurance.
There are 2 ways to register, and then to resolve such a case.
First, obvious way is the following
```C#
context.Register()
       .DependsOn<Insurance>(insuranceKey)
       .Provide<Tax>(taxKey, insurance => insurance.Taxes)
       .Exists();
```
In this case, the structure becomes a tree. For each insurance there will be a collection of matching assignments and a second collection of matching taxes. 
However, if you want to iterate through all the combinations and compare each assignment to each tax(I will use another synthetic condition here to demonstrate), then the tree structure is not good for you and you will need a chain structure, i.e. a tree with a single branch. This can be done the following way
```C#
context.Register()
       .DependsOn<Assignment>(assignmentKey)
	   .Resolves<Insurance>(insuranceKey)
       .Provide<Tax>(taxKey, insurance => insurance.Taxes)
       .Exists();
	   
context.For<Tax>(taxKey)
       .With<Assignment>(assignmentKey)
       .Filter((tax, assignment) => tax.Id == assignment.Id);
```
Here all the pairs of tax and assignment with matching ID's will be found. However, if you use this filter on the tree structure, a tax will be valid if its ID is equal to ID of the first assignment.

# Comparing two collections
The tree structure, allows to define a filter that uses 2 collections. It can't happen in the chain, because for every collection there is only a singular parent, and a singular parent of parent, there is no other collection that can be reached.
See the example of comparing 2 collections
```Cucumber
  And average payment per person in assignments B, specified in taxes B is over 10$
```
```C#
context
    .ForCollection<Assignment>(assignmentKey)
    .WithCollection<Tax>(taxKey)
    .Filter((assignments, taxes) => taxes.Sum(x => x.Value.Amount) / assignments.Sum(x => x.Value.HeadCount) > average);
```

# Break something
This testing technique has to be widely known, I just don't have a correct name for it.
The technique is to prepare a "happy path" case, check that it works, and then break one thing at a time to check the bad cases.
For example, for a happy path you can find and test the participant who has good password, email, access rights and so on.
You make sure that page works good for this participant. Then you might want to test that this page displays a warning/error/whatever if any data is missing.
For one test you get this "good" participant and break his password, for another test you break his email and so on. 
This does not test all the combinations of good and bad data, but I would not assume that the page was designed specifically to cheat its way through "negative" tests.
<br/>
Here is the example that I created using my model:
```Cucumber
Background: 
   Given insurance B is taken from policiesSource
	  And for insurance B exists a assignment B
	  And for insurance B exists a tax B

Scenario: No assignment with needed count and type
    Given condition 'AssignmentExists' is broken

Scenario: No tax with needed amount and type
    Given condition 'TaxExists' is broken
```
To implement such a behavior, any filter can be registered in the "Background" section with a key, and then "inverted" in the specific scenario.
So, for a happy path the filter evaluates to true for a insurance having a needed assignment(same for tax), and when you invert it, 
it evaluates to true for a insurance that does not have one.

# Logging a search failure
Sometimes, when many conditions are specified, it is not that obvious why the search does not yield any results. For that case there is an option to display the filter which invalidated the search last.
I.e. if you have 3 filters, and the first filter invalidates half the entities, the second filter invalidates the other half, then the third filter will not even be evaluated. In this case, there is a way to display information about that second filter.
To do that, you have to implement IResolutionLog interface and assign the instance to ResolutionLog property of the context prior to the search.

# Limitations
1. Combined filter can only reference either a singular parent in the same branch or an item/collection from the other branch. It can't reference a collection of a parent type, because no collection is availabe in the chain.
2. Combined filter can't reference a child of the node the filter belongs to.
2. Circular dependencies are not allowed. If the first branch references the second branch, then the second branch is not allowed to reference the first branch.
<br/>The search tool will throw a ResolutionException in case it finds one of these violations.

# Installation
Package is available on NuGet under the name "TestingContext".