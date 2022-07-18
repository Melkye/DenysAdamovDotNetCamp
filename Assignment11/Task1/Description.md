# Brief

Application is mostly based on interfaces for components to be easily interchangable.
I suppose there is some mistakes in excessive or incorrect use of interfaces

# Interfaces

## IGood : IComparable

An item of a storage.
Has Title, Mass, Price.
Has methods to change price and compare

## IStorage<T> : IEnumerable<T> where T : class, IGood

A storage of items of T
Has total mass, total price.
Has methods to fill it and retrieve element by index

## ILogger

A logger which allows retrieving and upadting entries

## IStorageService<T> : IEnumerable<T> where T : class, IGood

A business logic part which implements all operations with IStorage and ILogger

# Classes

## Product  : IGood

An abstract class which is a food product

## Meat : Product

A meat product which has its own type and category

## DairyProduct : Product

A dairy product which has its own number of days before spoil

## Storage<T> : IStorage<T> where T : class, IGood

A generic storage class implementing IStorage interface
Also has operators overloading

## StorageService : IStorageService<T> where T : class, IGood

A class to operate with a storage
Needs update because its method of reading from file is tied to Product class

## View

A presentation layer class which uses an IStorageService to display and change its content

## Startup

A class used to initialize a storage and run commands