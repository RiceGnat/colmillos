using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Colmillos
{
	[TestClass]
	public class PropertyManagerTests
	{
		private enum Property { A, B }

		private const string A_VALUE = "test";
		private const int B_VALUE = 1;

		private IPropertiesManager subject;

		[TestInitialize]
		public void Setup()
		{
			subject = new PropertyManager();
			subject[Property.A] = A_VALUE;
			subject[Property.B] = B_VALUE;

		}

		[TestMethod]
		public void SetAndGet()
		{
			Assert.AreEqual(A_VALUE, subject.GetProperty<string>(Property.A));
			Assert.AreEqual(B_VALUE, subject.GetProperty<int>(Property.B));
		}

		[TestMethod]
		public void Modifiers()
		{
			Mock<IPropertiesModifier> modifier = new Mock<IPropertiesModifier>();
			modifier.Setup(m => m.GetModifiedProperties(It.IsAny<IProperties>())).Returns<IProperties>(properties =>
			{
				Mock<IProperties> modified = new Mock<IProperties>();
				modified.Setup(p => p.GetProperty<string>(It.IsAny<Enum>())).Returns<Enum>(key => properties.GetProperty<string>(key) + "A");
				modified.Setup(p => p.GetProperty<int>(It.IsAny<Enum>())).Returns<Enum>(key => properties.GetProperty<int>(key) + 1);
				return modified.Object;
			});

			subject.Modifiers.Add(modifier.Object);

			Assert.AreEqual($"{A_VALUE}A", subject.GetProperty<string>(Property.A));
			Assert.AreEqual(2, subject.GetProperty<int>(Property.B));
		}
	}
}
