﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WordMemo.DataAccess.Contracts;
using WordMemo.DataAccess.Logic;
using WordMemo.DataAccess.Managers;
using WordMemo.ViewModels;

namespace WordMemo.UnitTests
{
    [TestFixture]
    public class WordLogicTests
    {
        public WordLogic mWordLogic;

        [SetUp]
        public void Init()
        {
            mWordLogic = new WordLogic(new PersistentWordManager<Word>(":memory:"));
            mWordLogic.UpdateWordList().Wait();
        }

        [Test]
        public async void word_is_updated_after_modifying_it()
        {
            // Arrange
            Word word = new Word(1, "testować", "to test");
            mWordLogic.WordList.Add(word);

            // Act
            await SaveWord();

            //Assert
            Assert.AreEqual("1. robić => do", mWordLogic.WordList[0].ToString());
        }

        public async Task SaveWord()
        {
            await mWordLogic.SaveWord(new Word("robić", "do"));
        } 
    }
}