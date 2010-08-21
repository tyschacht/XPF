//-------------------------------------------------------------------------------------------------
// <auto-generated> 
// Marked as auto-generated so StyleCop will ignore BDD style tests
// </auto-generated>
//-------------------------------------------------------------------------------------------------

#pragma warning disable 169
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local

namespace RedBadger.Xpf.Specs.Presentation.Controls.ItemsControlSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;

    using Machine.Specifications;

    using Moq;

    using RedBadger.Xpf.Graphics;
    using RedBadger.Xpf.Presentation;
    using RedBadger.Xpf.Presentation.Controls;

    using It = Machine.Specifications.It;

    public abstract class a_ItemsControl
    {
        protected static ItemsControl ItemsControl;

        private Establish context = () => ItemsControl = new ItemsControl();
    }

    [Subject(typeof(ItemsControl), "Panel")]
    public class when_a_panel_is_not_specified : a_ItemsControl
    {
        private It should_use_a_stack_panel = () => ItemsControl.ItemsPanel.ShouldBeOfType<StackPanel>();
    }

    [Subject(typeof(ItemsControl), "Item Template")]
    public class when_an_item_template_has_not_been_specified : a_ItemsControl
    {
        private static Exception exception;

        private static IList<Color> items;

        private Establish context = () =>
            {
                items = new List<Color> { Colors.Blue, Colors.Red };
                ItemsControl.ItemsSource = items;
            };

        private Because of = () => exception = Catch.Exception(() => ItemsControl.Measure(new Size()));

        private It should_throw_an_exception = () => exception.ShouldBeOfType<InvalidOperationException>();
    }

    [Subject(typeof(ItemsControl), "Item Template")]
    public class when_item_template_is_changed : a_ItemsControl
    {
        private static IList<Color> items;

        private Establish context = () =>
            {
                ItemsControl.ItemTemplate = () => new TextBlock(new Mock<ISpriteFont>().Object);
                items = new ObservableCollection<Color> { Colors.Blue };
                ItemsControl.ItemsSource = items;

                ItemsControl.Measure(new Size());
            };

        private Because of = () =>
            {
                ItemsControl.ItemTemplate = () => new Border();
                items.Add(Colors.Red);

                ItemsControl.Measure(new Size());
            };

        private It should_1_use_the_original_item_template_for_items_added_before_the_change =
            () => ItemsControl.ItemsPanel.Children[0].ShouldBeOfType<TextBlock>();

        private It should_2_use_the_new_item_template_for_items_added_after_the_change =
            () => ItemsControl.ItemsPanel.Children[1].ShouldBeOfType<Border>();
    }

    [Subject(typeof(ItemsControl), "Items Source")]
    public class when_items_source_has_not_been_specified : a_ItemsControl
    {
        private static Exception exception;

        private Because of = () => exception = Catch.Exception(() => ItemsControl.Measure(new Size()));

        private It should_not_throw_an_exception = () => exception.ShouldBeNull();
    }

    [Subject(typeof(ItemsControl), "Items Source")]
    public class when_items_source_is_changed : a_ItemsControl
    {
        private Establish context = () => ItemsControl.Measure(new Size());

        private Because of = () => ItemsControl.ItemsSource = new List<object>();

        private It should_invalidate_measure = () => ItemsControl.IsMeasureValid.ShouldBeFalse();
    }

    [Subject(typeof(ItemsControl), "Items Source - Non Observable")]
    public class when_items_source_is_not_observable_and_is_changed_to_a_new_non_observable_list : a_ItemsControl
    {
        private static IList<Color> newItems;

        private Establish context = () =>
            {
                ItemsControl.ItemsSource = new List<int> { 1, 2, 3 };
                ItemsControl.ItemTemplate = () => new TextBlock(new Mock<ISpriteFont>().Object);

                ItemsControl.Measure(new Size());

                newItems = new List<Color> { Colors.Blue };
            };

        private Because of = () =>
            {
                ItemsControl.ItemsSource = newItems;
                ItemsControl.Measure(new Size());
            };

        private It should_use_the_new_item_source_for_the_data_contexts =
            () => ItemsControl.ItemsPanel.Children[0].DataContext.ShouldEqual(Colors.Blue);

        private It should_use_the_new_items_source = () => ItemsControl.ItemsPanel.Children.Count.ShouldEqual(1);
    }

    [Subject(typeof(ItemsControl), "Items Source - Non Observable")]
    public class when_items_source_is_set_to_a_list_of_two_items : a_ItemsControl
    {
        private static IList<Color> items;

        private Establish context = () => items = new List<Color> { Colors.Blue, Colors.Red };

        private Because of = () =>
            {
                ItemsControl.ItemsSource = items;
                ItemsControl.ItemTemplate = () => new TextBlock(new Mock<ISpriteFont>().Object);

                ItemsControl.Measure(new Size());
            };

        private It should_lay_out_item_1_as_using_the_item_template =
            () => ItemsControl.ItemsPanel.Children[0].ShouldBeOfType<TextBlock>();

        private It should_lay_out_item_2_as_using_the_item_template =
            () => ItemsControl.ItemsPanel.Children[1].ShouldBeOfType<TextBlock>();

        private It should_set_the_data_context_on_element_1 =
            () => ItemsControl.ItemsPanel.Children[0].DataContext.ShouldEqual(Colors.Blue);

        private It should_set_the_data_context_on_element_2 =
            () => ItemsControl.ItemsPanel.Children[1].DataContext.ShouldEqual(Colors.Red);
    }

    [Subject(typeof(ItemsControl), "Items Source - Observable")]
    public class when_items_source_is_an_empty_observable_collection_and_a_new_item_is_added : a_ItemsControl
    {
        private static IList<Color> items;

        private Establish context = () =>
            {
                items = new ObservableCollection<Color>();
                ItemsControl.ItemsSource = items;

                ItemsControl.ItemTemplate = () => new TextBlock(new Mock<ISpriteFont>().Object);

                ItemsControl.Measure(new Size());
            };

        private Because of = () => items.Add(Colors.Blue);

        private It should_lay_it_out_using_the_item_template =
            () => ItemsControl.ItemsPanel.Children[0].ShouldBeOfType<TextBlock>();

        private It should_set_the_data_context =
            () => ItemsControl.ItemsPanel.Children[0].DataContext.ShouldEqual(Colors.Blue);
    }

    [Subject(typeof(ItemsControl), "Items Source - Observable")]
    public class when_items_source_is_bound_to_an_empty_observable_collection_and_a_new_item_is_added : a_ItemsControl
    {
        private static IList<Color> items;

        private Establish context = () =>
            {
                items = new ObservableCollection<Color>();
                ItemsControl.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = items });

                ItemsControl.ItemTemplate = () => new TextBlock(new Mock<ISpriteFont>().Object);

                ItemsControl.Measure(new Size());
            };

        private Because of = () => items.Add(Colors.Blue);

        private It should_lay_it_out_using_the_item_template =
            () => ItemsControl.ItemsPanel.Children[0].ShouldBeOfType<TextBlock>();

        private It should_set_the_data_context =
            () => ItemsControl.ItemsPanel.Children[0].DataContext.ShouldEqual(Colors.Blue);
    }

    [Subject(typeof(ItemsControl), "Items Source - Observable")]
    public class when_items_source_is_an_observable_collection_with_1_item_and_it_is_removed : a_ItemsControl
    {
        private static IList<Color> items;

        private Establish context = () =>
            {
                items = new ObservableCollection<Color> { Colors.Blue };
                ItemsControl.ItemsSource = items;

                ItemsControl.ItemTemplate = () => new TextBlock(new Mock<ISpriteFont>().Object);

                ItemsControl.Measure(new Size());
            };

        private Because of = () => items.RemoveAt(0);

        private It should_remove_the_element_from_the_panel =
            () => ItemsControl.ItemsPanel.Children.Count.ShouldEqual(0);
    }

    [Subject(typeof(ItemsControl), "Items Source - Observable")]
    public class when_items_source_is_an_observable_collection_with_1_item_and_it_is_replaced : a_ItemsControl
    {
        private static readonly Color expectedDataContext = Colors.Red;

        private static IList<Color> items;

        private static IElement oldElement;

        private Establish context = () =>
            {
                items = new ObservableCollection<Color> { Colors.Blue };
                ItemsControl.ItemsSource = items;

                ItemsControl.ItemTemplate = () => new TextBlock(new Mock<ISpriteFont>().Object);

                ItemsControl.Measure(new Size());

                oldElement = ItemsControl.ItemsPanel.Children[0];
            };

        private Because of = () => items[0] = expectedDataContext;

        private It should_have_the_same_number_of_elements_in_the_panel =
            () => ItemsControl.ItemsPanel.Children.Count.ShouldEqual(1);

        private It should_replace_the_element_in_the_panel =
            () => ItemsControl.ItemsPanel.Children[0].ShouldNotBeTheSameAs(oldElement);

        private It should_set_the_data_context_on_the_new_element =
            () => ItemsControl.ItemsPanel.Children[0].DataContext.ShouldEqual(expectedDataContext);
    }

    [Subject(typeof(ItemsControl), "Items Source - Observable")]
    public class when_items_source_is_an_observable_collection_with_2_items_and_the_first_item_is_moved_to_the_end :
        a_ItemsControl
    {
        private static IElement firstElement;

        private static ObservableCollection<Color> items;

        private static IElement lastElement;

        private Establish context = () =>
            {
                items = new ObservableCollection<Color> { Colors.Blue, Colors.Red };
                ItemsControl.ItemsSource = items;

                ItemsControl.ItemTemplate = () => new TextBlock(new Mock<ISpriteFont>().Object);

                ItemsControl.Measure(new Size());

                firstElement = ItemsControl.ItemsPanel.Children[0];
                lastElement = ItemsControl.ItemsPanel.Children[1];
            };

        private Because of = () => items.Move(0, 1);

        private It should_have_the_same_number_of_elements_in_the_panel =
            () => ItemsControl.ItemsPanel.Children.Count.ShouldEqual(2);

        private It should_move_the_first_element_in_the_panel_to_the_end =
            () => ItemsControl.ItemsPanel.Children[1].ShouldBeTheSameAs(firstElement);

        private It should_move_the_last_element_in_the_panel_to_the_front =
            () => ItemsControl.ItemsPanel.Children[0].ShouldBeTheSameAs(lastElement);
    }

    [Subject(typeof(ItemsControl), "Items Source - Observable")]
    public class when_items_source_is_an_observable_collection_with_1_item_and_it_is_cleared : a_ItemsControl
    {
        private static IList<Color> items;

        private Establish context = () =>
            {
                items = new ObservableCollection<Color> { Colors.Blue };
                ItemsControl.ItemsSource = items;

                ItemsControl.ItemTemplate = () => new TextBlock(new Mock<ISpriteFont>().Object);

                ItemsControl.Measure(new Size());
            };

        private Because of = () => items.Clear();

        private It should_not_have_any_elements_in_the_panel =
            () => ItemsControl.ItemsPanel.Children.Count.ShouldEqual(0);
    }

    [Subject(typeof(ItemsControl), "Items Source - Observable")]
    public class when_items_source_is_set_to_null_and_new_items_are_added_to_the_old_source : a_ItemsControl
    {
        private static ObservableCollection<Color> items;

        private Establish context = () =>
            {
                items = new ObservableCollection<Color> { Colors.Blue };
                ItemsControl.ItemsSource = items;

                ItemsControl.ItemTemplate = () => new TextBlock(new Mock<ISpriteFont>().Object);

                ItemsControl.Measure(new Size());
            };

        private Because of = () =>
            {
                ItemsControl.ItemsSource = null;
                items.Add(Colors.Yellow);

                ItemsControl.Measure(new Size());
            };

        private It should_clear_all_elements_from_the_panel_and_not_observe_the_old_source =
            () => ItemsControl.ItemsPanel.Children.Count.ShouldEqual(0);
    }

    [Subject(typeof(ItemsControl), "Items Source - Observable")]
    public class when_a_observable_item_source_is_replaced_with_a_non_observable : a_ItemsControl
    {
        private static ObservableCollection<Color> items;

        private Establish context = () =>
            {
                items = new ObservableCollection<Color> { Colors.Blue };
                ItemsControl.ItemsSource = items;

                ItemsControl.ItemTemplate = () => new TextBlock(new Mock<ISpriteFont>().Object);

                ItemsControl.Measure(new Size());
            };

        private Because of = () =>
            {
                ItemsControl.ItemsSource = new List<Color> { Colors.Pink };
                items.Add(Colors.Yellow);

                ItemsControl.Measure(new Size());
            };

        private It should_not_observe_the_old_source = () => ItemsControl.ItemsPanel.Children.Count.ShouldEqual(1);

        private It should_use_the_new_source =
            () => ItemsControl.ItemsPanel.Children[0].DataContext.ShouldEqual(Colors.Pink);
    }
}