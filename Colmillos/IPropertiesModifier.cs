namespace Colmillos
{
	public interface IPropertiesModifier : IEntity
	{
		IProperties GetModifiedProperties(IProperties properties);
	}
}
