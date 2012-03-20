using System;
using NUnit.Framework;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestFixture()]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[SetUp()]
		public void SetUp()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[Test()]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [Test()]
        public void TestGetCarLocation()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            String testA = "Terre-Haute";
            String testB = "Tucson, Arizona";

            using (mocks.Record())
            {
                mockDatabase.getCarLocation(10);
                LastCall.Return("Terre-Haute");

                mockDatabase.getCarLocation(1337);
                LastCall.Return("Tucson, Arizona");
            }

            var target = new Car(10);
           
            target.Database = mockDatabase;    
            Assert.AreEqual(testA, target.getCarLocation(10));
            Assert.AreEqual(testB, target.getCarLocation(1337));
        }

        [Test()]
        public void TestGetMilage()
        {
            int mileageA = 1337;
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            using (mocks.Record())
            {
                mockDatabase.Miles = mileageA;

            }
            var target = new Car(1);
            target.Database = mockDatabase;
            Assert.AreEqual(mileageA, target.Mileage);
        }
        [Test()]
        public void CarMotherTest()
        {
            
            Car BMWMother = ObjectMother.BMW();
            IDatabase mockDatabase = mocks.Stub<IDatabase>();

            using (mocks.Record())
            {
                mockDatabase.getCarLocation(10);
                LastCall.Return("Terre-Haute");
            }

            BMWMother.Database = mockDatabase;
            Assert.AreEqual("Terre-Haute", BMWMother.getCarLocation(10));

                
        }


	}
}
