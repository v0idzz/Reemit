using Reemit.Common;
using Reemit.Gui.ViewModels.Controls.HexEditor;

namespace Reemit.Gui.UnitTests.ViewModels.Controls.HexEditor
{
    public class HexEditorNavigationViewModelUnitTests
    {
        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(1, 2, true)]
        [InlineData(1, 3, true)]
        [InlineData(2, 2, true)]
        [InlineData(2, 3, true)]
        
        [InlineData(0, 1, false)]
        [InlineData(0, 2, false)]
        [InlineData(0, 3, false)]
        [InlineData(1, 4, false)]
        [InlineData(2, 4, false)]
        [InlineData(3, 3, false)]
        [InlineData(3, 4, false)]
        [InlineData(3, 5, false)]
        public void NavigationBitRange_Set_ResolvesRangeMapped(ulong start, ulong end, bool isExpected)
        {
            // Arrange
            var expectedRangeVM = CreateRangeViewModel(1, 2);

            var vm = new HexEditorNavigationViewModel();

            vm.NavigationRanges =
            [
                CreateRangeViewModel(0, 1),
                expectedRangeVM,
                CreateRangeViewModel(3, 2),
            ];

            // Act
            vm.NavigationBitRange = new AvaloniaHex.Document.BitRange(start, end);

            // Assert
            Assert.NotNull(vm);

            if (isExpected)
            {
                Assert.Equal(expectedRangeVM, vm.ResolvedNavigationRange);
            }
            else
            {
                Assert.NotEqual(expectedRangeVM, vm.ResolvedNavigationRange);
            }
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(0, 2)]
        [InlineData(1, 4)]
        [InlineData(3, 6)]
        [InlineData(4, 6)]
        [InlineData(5, 6)]
        [InlineData(50, 60)]
        public void NavigationBitRange_Set_DoesNotResolveRangeMapped(ulong start, ulong end)
        {
            // Arrange
            var vm = new HexEditorNavigationViewModel();

            vm.NavigationRanges =
            [
                CreateRangeViewModel(1, 2),
                CreateRangeViewModel(3, 2),
            ];

            // Act
            vm.NavigationBitRange = new AvaloniaHex.Document.BitRange(start, end);

            // Assert
            Assert.Null(vm.ResolvedNavigationRange);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(1, 3)]
        [InlineData(1, 4)]
        [InlineData(2, 2)]
        [InlineData(2, 3)]
        [InlineData(2, 4)]
        [InlineData(3, 3)]
        [InlineData(3, 4)]
        [InlineData(4, 4)]
        public void NavigationBitRange_Set_ResolvesInnerRangeMapped(ulong start, ulong end)
        {
            // Arrange
            var expectedRangeVM = CreateRangeViewModel(1, 3);

            var vm = new HexEditorNavigationViewModel();

            vm.NavigationRanges =
            [
                CreateRangeViewModel(0, 5),
                expectedRangeVM,
            ];

            // Act
            vm.NavigationBitRange = new AvaloniaHex.Document.BitRange(start, end);

            // Assert
            Assert.Equal(expectedRangeVM, vm.ResolvedNavigationRange);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(0, 2)]
        [InlineData(0, 3)]
        [InlineData(0, 4)]
        [InlineData(0, 5)]
        [InlineData(1, 5)]
        [InlineData(2, 5)]
        [InlineData(3, 5)]
        [InlineData(4, 5)]
        [InlineData(5, 5)]
        public void NavigationBitRange_Set_ResolvesOuterRangeMapped(ulong start, ulong end)
        {
            // Arrange
            var expectedRangeVM = CreateRangeViewModel(0, 5);

            var vm = new HexEditorNavigationViewModel();

            vm.NavigationRanges =
            [
                CreateRangeViewModel(1, 3),
                expectedRangeVM,
            ];

            // Act
            vm.NavigationBitRange = new AvaloniaHex.Document.BitRange(start, end);

            // Assert
            Assert.Equal(expectedRangeVM, vm.ResolvedNavigationRange);
        }

        private HexNavigationRangeViewModel CreateRangeViewModel(int position, int length) =>
            new HexNavigationRangeViewModel(
                new RangeMapped<string>(position, length, "foo"),
                () => { });
    }
}
