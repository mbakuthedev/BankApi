# OVERVIEW
It's basically an API that allows a transaction and gives response codes when the transactions fail on the client side and on the Banks side it allows the bank search Accounts transactions by account number , account name and even the type of account 
# BANK
So we have the Transaction model, that has properties like the TransactionId , transaction type , transaction status , A From account and destination account , a unique reference and a transaction date of type date. There's also a unique reference id that's instantiated and I used an enum to store related data like the transaction status which could be failed , success or an error. 
And then a transaction type which could be deposit , withdraw or transfer. 
That' explains my transaction models 
Then to the account model
The account model contains an accountId , accountname , accountnumber( which is auto generated) and generates a random 10 digit number on account creation , then there's also the account type which is an enum that contains the types of accounts eg current, savings , government and corporate accounts 
Ps: I won't be explaining all the models but I'll be explaining the important ones 
There's also a pinhash and pinsalt model that hashes and salts the pin so its secure ( it Uses a SHA512 hash)
Then there's an accountCreationDate and lastTransactionDate to enable the bank to track transaction dates and times 
Note: everything in the Account Model is accessible by only the bank 
But for the bank to get acces to all these , I created something like a Dto to also filter out what requests can be made by a bank worker 
Filtering the requests that can be made was where I used the GetAccountModel which allows a bank worker to request information using the Account name, Account number, Acccount Id , Email , Account balance and even account types  
# USER 
Now to the part where Users can use 
To register an account, a User needs the following information :
first name , lastname, email, phone number and a type of account to be created ie current , savings etc. Pin, and a confirm pin. 
So I made the pin accept a 4 digit pin between 0-9
Then there's a response model that holds the response code and a response Id which is a guid and a response message that either returns error messages. 
Then going on to the Transaction model 
That's what facilities all transactions and in the model , we have properties which are The Destination Account , the Source Account , the Transaction type , Transaction date, Transaction Status and even a Transaction Unique reference which was instantiated to be a guid 
And then there's the Transaction Request Dto which is also a model that allows the Users to search for their transactions ( more like a transaction history) 
Then the last model will be an Update model that is accessible to the Users and allows them change their pins, phone numbers and emails 
#HOW TO
Switch to master branch
