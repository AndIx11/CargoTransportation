using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DatabaseManagers
{
	public static class DataVerification
	{
		/// <summary>
		/// Метод для проверки, что строка не содержит цифр или некорректных символов
		/// </summary>
		public static bool IsValidString(string input)
		{
			if (string.IsNullOrEmpty(input))
				return false;

			// Регулярное выражение для проверки наличия только букв, пробелов, 
			// дефисов и символов " и '
			Regex regex = new Regex(@"^[a-zA-Zа-яА-ЯёЁ\s\-\""'']*$");
			return regex.IsMatch(input);
		}

		/// <summary>
		/// Метод для проверки, что строка состоит только из цифр
		/// </summary>
		public static bool IsDigitsOnly(string input)
		{
			if (string.IsNullOrEmpty(input))
				return false;

			// Регулярное выражение для проверки наличия только цифр
			Regex regex = new Regex(@"^\d+$");
			return regex.IsMatch(input);
		}

		/// <summary>
		/// Метод для проверки, содержатся ли в строке цифры
		/// </summary>
		public static bool ContainsDigits(string input)
		{
			if (string.IsNullOrEmpty(input))
				return false;

			// Регулярное выражение для поиска цифр
			Regex regex = new Regex(@"\d");
			return regex.IsMatch(input);
		}
	}
}
