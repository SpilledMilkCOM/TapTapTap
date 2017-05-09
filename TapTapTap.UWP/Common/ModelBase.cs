using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SM.Common.Models
{
	public class ModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		// Poor man's subject/observer
		private Dictionary<string, HashSet<string>> _propertyDependencies;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		/// <summary> First property depends on the changed value of the second property.
		///		If the second property (subject) is set using SetProperty, then the dependent (observer) property will be notified as well.
		/// <example>
		///					AddPropertyDependency(() => ThisProperty, () => DependsOnThisProperty);
		/// </example>
		///		The benefits of this is that we can SEE DIRECTLY in the code which property is related to what.
		///		DO NOT use RaiseDependentPropertyChanged because it's HARDER to see which property is related to which property.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="U"></typeparam>
		/// <param name="observerProperty"></param>
		/// <param name="subjectProperty"></param>
		protected void AddPropertyDependency(string dependentName, string sourceName)
		{
			if (_propertyDependencies == null)
			{
				_propertyDependencies = new Dictionary<string, HashSet<string>>();
			}

			//TODO: Check for recursion.

			if (dependentName == sourceName)
			{
				throw new ArgumentOutOfRangeException("dependentProperty", "The dependent and source property cannot be the same.");
			}

			HashSet<string> dependents = CacheUtility.FindInCacheOrAdd(_propertyDependencies, sourceName, () => new HashSet<string>());

			if (!dependents.Contains(dependentName))
			{
				dependents.Add(dependentName);
			}
		}

		/// <summary> Set the value of the backing variable and notify based on the property name (CallerMemberName)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">Source "value"</param>
		/// <param name="destination">The backing variable.</param>
		/// <param name="propertyName">Defaults to [CallerMemberName]</param>
		/// <returns></returns>
		protected bool SetValue<T>(T source, ref T destination, [CallerMemberName] string propertyName = null)
		{
			bool changed = false;

			if ((source == null && destination != null)
			|| (source != null && destination == null)
			|| (source != null && !source.Equals(destination)))
			{
				destination = source;
				changed = true;

				OnPropertyChanged(propertyName);
				RaiseDependentsPropertyChanged(propertyName);
			}

			return changed;
		}

		//----==== PRIVATE ====--------------------------------------------------------------------

		private void RaiseDependentsPropertyChanged(string propertyName)
		{
			if (_propertyDependencies != null)
			{
				HashSet<string> dependents = CacheUtility.FindInCache(_propertyDependencies, propertyName);

				if (dependents != null)
				{
					foreach (string dependent in dependents)
					{
						OnPropertyChanged(dependent);
						RaiseDependentsPropertyChanged(dependent);
					}
				}
			}
		}
	}
}