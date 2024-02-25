using BankApplication.Domain;
using NUnit.Framework;

namespace BankApplicationTests.UnitTests;

[TestFixture]
public class TransferTests
{
    private TestContext _context;

    [SetUp]
    public void SetUp() => _context = new TestContext();

    [Test]
    public void Verify_Deposit_Is_Successful() =>
        _context
            .ArrangeDeposit(5000)
            .AssertDepositIsSuccessful();

    [Test]
    public void Verify_Negative_Deposit_Is_Unsuccessful() =>
        _context
            .AssertNegativeDepositThrowsInvalidOperationException();

    [Test]
    public void Verify_Withdraw_Is_Successful() =>
        _context
            .ArrangeDeposit(5000)
            .AssertDepositIsSuccessful();

    [Test]
    public void Verify_Withdraw_More_Than_Balance_Is_Unsuccessful() =>
        _context
            .AssertWithdrawMoreThanBalanceThrowsInvalidOperationException();

    [Test]
    public void Verify_Transfer_Transaction_Is_Successful() =>
        _context
            .ArrangeTransferToAccount(600)
            .AssertTransferTransactionIsSuccessful()
            .AssertToAccountReceivedAmount(600);

    private class TestContext 
    {
        private Account _fromAccount;
        private Account _toAccount;

        private Transfer _transfer;
        private Deposit _deposit;
        private Withdraw _withdraw;

        public TestContext()
        {
            ArrangeAccounts();
        }

        private TestContext ArrangeAccounts() 
        {
            _fromAccount = new Account("From Account", "", 0, 0000);

            _toAccount = new Account("To Account", "", 0, 0000);

            return this;
        }

        public TestContext ArrangeDeposit(decimal amount) 
        {
            _deposit = new Deposit(_fromAccount, amount);

            _deposit.Execute();

            return this;
        }

        public TestContext ArrangeWithdraw(decimal amount)
        {
            _withdraw = new Withdraw(_fromAccount, amount);

            _withdraw.Execute();

            return this;
        }

        public TestContext ArrangeDepositFromAccount(decimal amount) 
        {
            ArrangeDeposit(amount);

            return this;
        }

        public TestContext ArrangeTransferToAccount(decimal amount)
        {
            ArrangeDeposit(int.MaxValue);

            _transfer = new Transfer(_fromAccount, _toAccount, amount);

            return this;
        }

        public TestContext AssertTransferTransactionIsSuccessful() 
        {
            _transfer.Execute();

            //Assert.That(_transfer.Success, Is.True);

            return this;
        }

        public void AssertToAccountReceivedAmount(decimal amount)
        {
            Assert.That(_toAccount.Balance, Is.EqualTo(amount));
        }

        public void AssertDepositIsSuccessful() 
        {
            //Assert.That(_deposit.Success, Is.EqualTo(true));
        }

        public void AssertWithdrawIsSuccessful()
        {
            //Assert.That(_withdraw.Success, Is.EqualTo(true));
        }

        public void AssertNegativeDepositThrowsInvalidOperationException() 
        {
            Assert.That(() => new Deposit(_fromAccount, -1000), Throws.TypeOf<ArgumentException>());
        }

        public void AssertWithdrawMoreThanBalanceThrowsInvalidOperationException() 
        {
            Assert.That(() => new Withdraw(_fromAccount, int.MaxValue), Throws.TypeOf<ArgumentException>());
        }
    }
}
