// Copyright Edanoue, Inc. All Rights Reserved.

#nullable enable
using System;
using System.Linq;
using NUnit.Framework;

namespace Edanoue.ComponentSystem.Tests
{
    public class TS_Accessor
    {
        [Test]
        [Category("Normal")]
        public void GetFeatureのテスト()
        {
            var button = new Button();
            var collector = EdaFeatureCollector.Create(button);

            // Feature の取得ができる
            var feature = collector.GetFeature<IFeatureButton>();
            Assert.That(feature, Is.Not.Null);
            Assert.That(feature, Is.EqualTo(button));
        }

        [Test]
        [Category("Normal")]
        public void TryGetFeatureのテスト()
        {
            var button = new Button();
            var collector = EdaFeatureCollector.Create(button);

            // Feature の取得ができる
            var success = collector.TryGetFeature<IFeatureButton>(out var feature);
            Assert.That(success, Is.True);
            Assert.That(feature, Is.Not.Null);
            Assert.That(feature, Is.EqualTo(button));
        }

        [Test]
        [Category("Normal")]
        public void GetFeatureは最初のものが取得できる()
        {
            // 同じ Feature をもつインスタンスを複数個登録する
            var buttonA = new Button();
            var buttonB = new Button();
            var collector = EdaFeatureCollector.Create(buttonA, buttonB);

            // Feature の取得ができる
            // 単体取得版では最初に登録したものが取得できる
            var feature = collector.GetFeature<IFeatureButton>();
            Assert.That(feature, Is.Not.Null);
            Assert.That(feature, Is.EqualTo(buttonA));

            // TryGetFeature でも同様
            collector.TryGetFeature<IFeatureButton>(out var featureB);
            Assert.That(featureB, Is.Not.Null);
            Assert.That(featureB, Is.EqualTo(buttonA));
        }

        [Test]
        [Category("Normal")]
        public void GetFeaturesのテスト()
        {
            var buttonA = new Button();
            var buttonB = new Button();
            var collector = EdaFeatureCollector.Create(buttonA, buttonB);

            // 複数取得, 2つの Feature が取得できている
            var featuresA = collector.GetFeatures<IFeatureButton>();
            Assert.That(featuresA.Count(), Is.EqualTo(2));
        }

        [Test]
        [Category("Normal")]
        public void TryGetFeaturesのテスト()
        {
            var buttonA = new Button();
            var buttonB = new Button();
            var collector = EdaFeatureCollector.Create(buttonA, buttonB);

            // 複数取得(Try 版), 2つの Feature が取得できている
            var success = collector.TryGetFeatures<IFeatureButton>(out var featuresB);
            Assert.That(success, Is.True);
            Assert.That(featuresB.Count(), Is.EqualTo(2));
        }

        [Test]
        [Category("Normal")]
        public void Accessor同士がアクセスできる()
        {
            var button = new Button();
            var counter = new Counter();
            EdaFeatureCollector.Create(button, counter);

            // ボタンを押すとカウンタが更新される
            Assert.That(counter.GetCount(), Is.EqualTo(0));
            button.Push();
            Assert.That(counter.GetCount(), Is.EqualTo(1));
        }

        private interface IFeatureButton : IEdaFeature
        {
            public void Push();
            public event Action Pushed;
        }

        private interface IFeatureCounter : IEdaFeature
        {
            public int GetCount();
        }

        private class Counter : IEdaFeatureAccessor, IFeatureCounter
        {
            private int _counter;

            public void AddFeatures(IWriteOnlyEdaFeatureCollector collector)
            {
                collector.AddFeature<IFeatureCounter>(this);
            }

            public void GetFeatures(IReadOnlyEdaFeatureCollector collector)
            {
                var button = collector.GetFeature<IFeatureButton>();
                if (button is not null)
                {
                    button.Pushed += () => { _counter++; };
                }
            }

            public int GetCount()
            {
                return _counter;
            }
        }

        private class Button : IEdaFeatureAccessor, IFeatureButton
        {
            public void AddFeatures(IWriteOnlyEdaFeatureCollector collector)
            {
                collector.AddFeature<IFeatureButton>(this);
            }

            public void GetFeatures(IReadOnlyEdaFeatureCollector collector)
            {
            }

            public void Push()
            {
                Pushed?.Invoke();
            }

            public event Action? Pushed;
        }
    }
}