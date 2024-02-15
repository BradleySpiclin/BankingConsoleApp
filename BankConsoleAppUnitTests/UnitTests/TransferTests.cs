using BankingConsoleApp;
using NUnit.Framework;

namespace BankApplicationUnitTests.UnitTests;

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
    public void Verify_Withdraw_Is_Unsuccessful() =>
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

        private TransferTransaction _transferTransaction;
        private DepositTransaction _depositTransaction;
        private WithdrawTransaction _withdrawTransaction;

        public TestContext()
        {
            ArrangeAccounts();
        }

        private TestContext ArrangeAccounts() 
        {
            _fromAccount = new Account("From Account", 0);

            _toAccount = new Account("To Account", 0);

            return this;
        }

        public TestContext ArrangeDeposit(decimal amount) 
        {
            _depositTransaction = new DepositTransaction(_fromAccount, amount);

            _depositTransaction.Execute();

            return this;
        }

        public TestContext ArrangeWithdraw(decimal amount)
        {
            _withdrawTransaction = new WithdrawTransaction(_fromAccount, amount);

            _withdrawTransaction.Execute();

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

            _transferTransaction = new TransferTransaction(_fromAccount, _toAccount, amount);

            return this;
        }

        public TestContext AssertTransferTransactionIsSuccessful() 
        {
            _transferTransaction.Execute();

            Assert.That(_transferTransaction.Success, Is.True);

            return this;
        }

        public void AssertToAccountReceivedAmount(decimal amount)
        {
            Assert.That(_toAccount.Balance, Is.EqualTo(amount));
        }

        public void AssertDepositIsSuccessful() 
        {
            Assert.That(_depositTransaction.Success, Is.EqualTo(true));
        }

        public void AssertWithdrawIsSuccessful()
        {
            Assert.That(_withdrawTransaction.Success, Is.EqualTo(true));
        }

        public void AssertNegativeDepositThrowsInvalidOperationException() 
        {
            Assert.That(() => new DepositTransaction(_fromAccount, -1000), Throws.TypeOf<ArgumentException>());
        }

        public void AssertWithdrawMoreThanBalanceThrowsInvalidOperationException() 
        {
            Assert.That(() => new WithdrawTransaction(_fromAccount, int.MaxValue), Throws.TypeOf<ArgumentException>());
        }
    }
}
