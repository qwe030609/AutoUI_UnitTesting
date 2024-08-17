using System;
using System.Collections.Concurrent;
using OpenQA.Selenium.Appium.Windows;
using PP5AutoUITests.Session;

namespace PP5AutoUITests
{
    public class DriverFactory
    {
        /// <summary>
        /// Drivers 集合
        /// </summary>
        private ConcurrentDictionary<SessionType, IDriver> allDrivers = new ConcurrentDictionary<SessionType, IDriver>();
        private static DriverFactory driverFactory;
        DriverFactory()
        {
            this.CreateFactory();
        }

        public static DriverFactory GetInstance()
        {
            if (driverFactory == null)
                driverFactory = new DriverFactory();

            return driverFactory;
        }

        /// <summary>
        /// 依照 Session Type 建立公開的 Driver
        /// </summary>
        /// <param name="tableName">要建立的資料表名稱</param>
        /// <returns>回傳建立的資料表</returns>
        public WindowsDriver<WindowsElement> Create(SessionType sessionType)
        {
            if (!this.allDrivers.ContainsKey(sessionType))
            {
                throw new ArgumentOutOfRangeException(sessionType.ToString());
            }
            return this.allDrivers[sessionType].CreateDriver();
        }

        /// <summary>
        /// 依照 Session Type attch 至對應的 Driver
        /// </summary>
        /// <param name="tableName">要建立的資料表名稱</param>
        /// <returns>回傳建立的資料表</returns>
        public WindowsDriver<WindowsElement> Attach(SessionType sessionType)
        {
            if (!this.allDrivers.ContainsKey(sessionType))
            {
                throw new ArgumentOutOfRangeException(sessionType.ToString());
            }
            return this.allDrivers[sessionType].AttachExistingDriver();
        }

        /// <summary>
        /// 依照 Session Type 取得已建立的 Driver
        /// </summary>
        /// <param name="tableName">要建立的資料表名稱</param>
        /// <returns>回傳建立的資料表</returns>
        public IDriver Get(SessionType sessionType)
        {
            if (!this.allDrivers.ContainsKey(sessionType))
            {
                throw new ArgumentOutOfRangeException(sessionType.ToString());
            }
            return this.allDrivers[sessionType];
        }

        /// <summary>
        /// 初始化資料表處理物件集合
        /// </summary>
        private void CreateFactory()
        {
            this.allDrivers.TryAdd(SessionType.MainPanel, new MainPanelDriver());
            this.allDrivers.TryAdd(SessionType.PP5IDE, new PP5IDEDriver());
        }
    }
}
