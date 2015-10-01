# TestingContext
A tool for advanced data search, designed for use with SpecFlow.

# Historical reasons
Most of the time, when you test something, it makes sense to create test data for your testcase.
Here is the example of how it usually works. You write a SpecFlow scenario like this
```Cucumber
Given that i have a policy named 'somename'
  And policy has a coverage with type 'Dependent' and over 70 people covered
```
In the backend, you create a code like this
```C#
policy = new Policy { Name = policyName, Coverages = new List<Coverage>() };
policy.Coverages.Add(new Coverage { Type = coverageType, HeadCount = headCount + 1 });
```
First line of such a code goes to the definition of a first SpecFlow step and second line goes for definition of a second step. Of course there are some means to reach that policy object and share it between steps, i am not mentioning any of that here to not flood the article with details, trying to show only meaningful parts of code.
Anyway. Sometimes, it so happens that you need to test an application, web portal for example and it has a lot of data consumed and displayed on screen. And, even though you only want to test small part of it, you will have to create a lot of data, so that other parts, displayed on html page would not fail because of no data, so to set up such a test you will need to do a lot of code and, which is even worse, a lot of lines in scenario file.

Another approach can be taken to test an application like that. This another approach would not work at first if you are working on application from the ground up, for obvious reasons, but it fits great if you already have a lot of functionality, and you are adding new, or modifying existing functionality. Approach is to have existing test data to choose from, using specified conditions. It can be a test database, or a set of 'mock' files, containing the data. You can reroute implementation of service interfaces from real db services to mocking services, loading and deserializing data from such a file.
So, to test the feature, you would still need 'somename' policy that has 70 dependents covered, but you will not create the policy, and instead you choose from policies that you already have in database, which has a lot of other data filled in, besides coverage. Taxes, annual premiums, renewal proposals and so on, the model in our application was using over 20 db tables. I am using very simplified model here, which is enough to demonstrate the concepts.
But lets say each policy has a lot of data that you don't need to create, check, or even be aware of its existense, because it is not needed for the test at hand.
Anyway, the C# step definitions gets changed to look like the following
```C#
policies = policies.Where(x => x.Name == policyName);
policies = policies.Where(x => x.Coverages.Any(y => y.Type == coverageType && y.HeadCount > headCount));
```
Of course you will need to define policies first, in pre-scenario hook, or in explicit extra step definition (which i personally think is much better, explicit over implied).
After these steps, you create a step that opens a portal with policyId parameter fit into http request line, taken from first policy in policies enumerable, and then test what you intended in subsequent steps

# Advanced search
So you did find a policy that you need, now you are testing the UI. You checked that policy name is displayed in some section on the ui, now you need to check that some UI section displays that seventy-something number of dependents. And there you hit a problem. You have a reference to a policy, but how would you know which of, let's say, five coverage lines did trigger the predicate for the policy. Which coverage would you take to compare its headcount to the number displayed on UI? 
You could specify the coverage condition the second time in the "Then" scenario step, or you could make a separate step for each scenario. Both of these approches did not seem viable
