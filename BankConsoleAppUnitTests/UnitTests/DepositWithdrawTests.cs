using BankApplication.Domain;
using NUnit.Framework;

namespace BankApplicationTests.UnitTests;

[TestFixture]
public class DepositWithdrawTests
{
    private TestContext _context;

    [SetUp]
    public void Setup()
    {
        _context = new TestContext();
    }

    [Test]
    public void Verify_Deposit() => 
        _context
            .ArrangeDepositAmount(100)
            .AssertBalanceEquals(100);

    [Test]
    public void Verify_Withdraw() =>
        _context
            .ArrangeDepositAmount(100)
            .ArrangeWithdrawAmount(100)
            .AssertBalanceEquals(0);

    [Test]
    public void Verify_No_Balance_Cannot_Withdraw() =>
        _context
            .ArrangeWithdrawAmount(100)
            .AssertWithdrawIsFalse(200);

    [Test]
    public void Verify_Cannot_Withdraw_More_Than_Balance() =>
        _context
            .ArrangeDepositAmount(10000)
            .AssertWithdrawIsFalse(50000);

    private class TestContext
    {
        private readonly Account _account;

        public TestContext() 
        {
            _account = new Account("Test Account", "", 0, 0000);
        }

        public TestContext ArrangeDepositAmount(decimal amount) 
        {
            _account.Deposit(amount);

            return this;
        }

        public TestContext ArrangeWithdrawAmount(decimal amount)
        {
            _account.Withdraw(amount);

            return this;
        }

        public void AssertBalanceEquals(decimal balance)
        {
            Assert.That(_account.Balance, Is.EqualTo(balance));
        }

        public void AssertWithdrawIsFalse(decimal balance)
        {
            Assert.That(_account.Withdraw(balance), Is.EqualTo(false));
        }
    }
}