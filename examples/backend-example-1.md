# Backend Example 1: REST API endpoint with string validation

## Description
This exercise is about implementing a REST API that can do string validations. It has to implemented in **C#**, using the **latest asp.net Version**.

The details of this example are not fully defined by purpose. If something in the specification is missing it is up to you, to implement best practices, make meaningful assumptions or ask for missing details.

Beside of the implementation, think also about necessary technical documentation (e.g. Readme.md, API Spec,...). Furthermore automatic testing is an integral part of implementation.

## Parantheses (well-formedness) Checker
Implement a REST endpoint taking a string expression to validate for well-formedness. The validation should examine the order and pairs of paranthesis: ```(```, ```[```, ```{```, ```)```, ```]```, ```}```

### Validation rules:
- Every opening paranthesis has a closing paranthesis of the same type
   - ```(``` is closed by ```)```
   - ```[``` is closed by ```]```
   - ```{``` is closed by ```}```
- The order of the closing parantheses has to be correct, if there are different paratheses

### Valid Examples
- ```Lorem ( ipsum ) dolor```
- ```Lorem [ipsum (dolor sit) amet], consectetur```
- ```()```
- ```Lorem [{ipsum [dolor]} sit] amet, consectetur ()```

### Invalid Examples
- ```Lorem (ipsum dolor```
- ```(Lorem]```
- ```Lorem [ipsum (dolor] sit) amet, consectetur```
- ```({)}```

# Further considerations
- Edge cases
- Containerization (e.g. Docker)
- Observability (Logging, Monitoring,...)