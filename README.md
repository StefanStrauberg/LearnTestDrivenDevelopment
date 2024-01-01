# LearnTestDrivenDevelopment
Main goal of this repo is the view about what is the TDD approach and how it implements

## What is Test Driven Development
- Test-driven development (TDD) is a software development process on software requirement being written as test cases before real code writen.
- The process of ensuring that just enough code is written to fulfill a requirement.
- A methods that introduces testing upfront, giving your code better long term coverage.
- Guards your code against potential breaking future modifications.

<div>
  <p align="center">
    <img src="/Images/TDD_1.png" width="600"/>  
  </p>
</div>

## Why is Unit Testing Important
- Makes you think about writing **testable code**.
- Usually leads to clean and succinct code and the use of good practices.
- Helps you to test code more quickly, to match against requirements.
- You can start building the business logic from earlier, even before building an applications.
- Helps to guard code against potential breaking changes.
- Can be used as documentation for existing code. Test scenarios can help to explain why the code was written the way it was.

## Unit Testing Disadvantages
- Time Consuming. It forces a developer to write almost twice the amount of code.
- Might be difficult for beginners initially.

## What we will learn
- Use **xUnit** Test Projects.
- Use **Moq** and **Shouldly** to write unit tests.
- Conduct **Red**, **Green**, **Refactor** Test-driven development (**TDD**).
- Practice **Test-Driven Development** in real code.
- Learn to write **testable code**.
- Review Pitfalls to avoid and common challenges.

## Understanding Our Project Requirements
- A Room Booking Application.
- Booking requires Full Name, Email Address, Phone Number, Booking Date.
- Process Booking Request based on available rooms and returns data with success flag or not.
- Store Successful booking in database.
- Web interface for Booking.