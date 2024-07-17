namespace Template.DesignPatterns.Singleton
{
	/// <summary> C#クラス用のシングルトン </summary>
	public class Singleton<T> where T : class, new()
	{
		static readonly System.Lazy<T> instance = new System.Lazy<T>(() => new T());

		public static T Instance
		{
			get
			{
				return instance.Value;
			}
		}

		protected Singleton() { }
	}
}
